using AutoMapper;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using LuminaApp.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetBulletinByStudent
{
    public class GetBulletinByStudentQueryHandler : IRequestHandler<GetBulletinByStudentQuery, ICollection<BulletinDTO>>
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly ISubjectService _subjectService;
        private readonly IEvaluationService _evaluationService;
        private readonly IMapper _mapper;

        public GetBulletinByStudentQueryHandler(IGenericRepository<User> userRepo, ISubjectService subjectService, IEvaluationService evaluationService, IMapper mapper)
        {
            _userRepo = userRepo;
            _subjectService = subjectService;
            _evaluationService = evaluationService;
            _mapper = mapper;
        }

        public async Task<ICollection<BulletinDTO>> Handle(GetBulletinByStudentQuery request, CancellationToken cancellationToken)
        {
            User student = await _userRepo.GetByIdAsync(request.StudentId);
            var evaluations = await _evaluationService.GetEvaluationsByStudent(request.StudentId);
            ICollection<BulletinDTO> bulletinDTOs = new List<BulletinDTO>();

            ICollection<Subject> subjectsByStudent = await _subjectService.GetSubjectsByGrade(student.grade.GradeId);

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            // Determine start and end dates based on the semester parameter
            switch (request.Semester)
            {
                case 1:
                    startDate = new DateTime(DateTime.Now.Year, 9, 15);
                    endDate = new DateTime(DateTime.Now.Year, 12, 15);
                    break;
                case 2:
                    startDate = new DateTime(DateTime.Now.Year, 1, 2);
                    endDate = new DateTime(DateTime.Now.Year, 3, 15);
                    break;
                case 3:
                    startDate = new DateTime(DateTime.Now.Year, 4, 1);
                    endDate = new DateTime(DateTime.Now.Year, 7, 30);
                    break;
            }

            foreach (Subject subject in subjectsByStudent)
            {
                ICollection<Evaluation> evaluationsBySubject = await _evaluationService.GetEvaluationsBySubjectAndStudent(request.StudentId, subject.subjectId);

                // Filter evaluations based on the semester dates
                evaluationsBySubject = evaluationsBySubject.Where(ev => ev.session.end_hour.Date >= startDate && ev.session.end_hour.Date <= endDate).ToList();

                // Ensure there's at least one evaluation of each type (Controle, Synthese, Orale)
                var controleEvaluations = evaluationsBySubject.Where(ev => ev.evaluationType == EvaluationType.Controle).ToList();
                var syntheseEvaluations = evaluationsBySubject.Where(ev => ev.evaluationType == EvaluationType.Synthese).ToList();
                var oraleEvaluations = evaluationsBySubject.Where(ev => ev.evaluationType == EvaluationType.Orale).ToList();

                if (controleEvaluations.Any() && syntheseEvaluations.Any() && oraleEvaluations.Any())
                {
                    // Calculate averages for each evaluation type and round to two decimal places
                    float controleAverage = (float)(controleEvaluations.Any() ? Math.Round(controleEvaluations.Average(ev => ev.Mark ?? 0), 2) : 0);
                    float syntheseAverage = (float)(syntheseEvaluations.Any() ? Math.Round(syntheseEvaluations.Average(ev => ev.Mark ?? 0), 2) : 0);
                    float oraleAverage = (float)(oraleEvaluations.Any() ? Math.Round(oraleEvaluations.Average(ev => ev.Mark ?? 0), 2) : 0);

                    // Calculate overall result for the subject and round to two decimal places
                    float subjectResult = (float)Math.Round((controleAverage + syntheseAverage * 2 + oraleAverage) / 4, 2);

                    // Create BulletinDTO for the subject
                    BulletinDTO bulletinDTO = new BulletinDTO
                    {
                        SubjectName = subject.SubjectName,
                        coef = subject.coefficient,
                        ControleResult = controleAverage,
                        SyntheseResult = syntheseAverage,
                        OraleResult = oraleAverage,
                        Result = subjectResult,
                        Teacher = subject.teacher.UserName
                    };

                    // Map BulletinDTO to the DTO object
                    BulletinDTO mappedBulletinDTO = _mapper.Map<BulletinDTO>(bulletinDTO);

                    // Add mapped BulletinDTO to the list
                    bulletinDTOs.Add(mappedBulletinDTO);
                }
            }

            return bulletinDTOs;
        }
    }
}

