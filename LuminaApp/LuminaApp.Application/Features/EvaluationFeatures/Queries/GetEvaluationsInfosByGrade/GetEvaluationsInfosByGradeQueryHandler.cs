using AutoMapper;
using LuminaApp.Application.Features.EvaluationFeatures.Dtos;
using LuminaApp.Application.Interfaces;
using LuminaApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaApp.Application.Features.EvaluationFeatures.Queries.GetEvaluationsInfosByGrade
{
    public class GetEvaluationsInfosByGradeQueryHandler:IRequestHandler<GetEvaluationsInfosByGradeQuery,ICollection<EvaluationDTO>>
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IEvaluationService _evaluationService;
        private readonly IMapper _mapper;

        public GetEvaluationsInfosByGradeQueryHandler(IGenericRepository<User> userRepo,IEvaluationService evaluationService, IMapper mapper)
        {
            _userRepo = userRepo;
            _evaluationService = evaluationService;
            _mapper = mapper;
        }

        public async Task<ICollection<EvaluationDTO>> Handle(GetEvaluationsInfosByGradeQuery request, CancellationToken cancellationToken)
        {
            User student= await _userRepo.GetByIdAsync(request.StudentId);
            Grade studentGrade= student.grade;
            List<float?> classEvas= new List<float?>();
            ICollection<Evaluation> studentEvas = await _evaluationService.GetEvaluationsByStudent(request.StudentId);
            var evaluationDTOs = _mapper.Map<ICollection<EvaluationDTO>>(studentEvas);
            if (studentGrade != null ) {
                foreach (EvaluationDTO evaluation in evaluationDTOs)
                {
                    foreach(User collegue in studentGrade.students)
                    {
                        if (collegue.evaluations != null) { 
                        foreach(Evaluation collegueEva in collegue.evaluations)
                        {
                            if (collegueEva.session.subject.SubjectName.Equals(evaluation.SubjectName) && 
                                collegueEva.session.start_hour.Day.Equals(evaluation.EvaluationDate.Day)&&collegueEva.Mark!=null)
                            {
                                classEvas.Add(collegueEva.Mark);
                            }
                        }
                        }
                    }
                    evaluation.ClassAverage = classEvas.Average();
                    evaluation.ClassHeighestMark = classEvas.Max();
                    evaluation.ClassLowestMark = classEvas.Min();
                }
                return evaluationDTOs;
            }
            else
            {
                throw new Exception("l'élève n'a pas encore été affecté à une classe");
            }
        }
    }
}
