using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Application.Features.FolderFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuminaGed.Application.Features.DocumentsFeatures.Queries.GetHomeworks
{
    public class GetHomeworksQueryHandler : IRequestHandler<GetHomeworksQuery, ICollection<HomeworksDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDocService _docService;

        public GetHomeworksQueryHandler(IMapper mapper,IDocService docService)
        {
            _mapper = mapper;
            _docService = docService;
        }

        public async Task<ICollection<HomeworksDto>> Handle(GetHomeworksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var documents = await _docService.GetLatestHomeworks(request.StudentId);
                var documentDtos = documents.Select(doc =>
                {
                    var homeworkDto = _mapper.Map<HomeworksDto>(doc);
                    homeworkDto.status = doc.folder.Documents.Any(d => d.studentFK == request.StudentId) ? "fait" : "non fait";
                    return homeworkDto;
                }).ToList();

                return documentDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
