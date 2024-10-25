using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LuminaApp.Application.Commons.Exceptions;
using LuminaGed.Application.Features.DocumentsFeatures.Dtos;
using LuminaGed.Application.Interfaces;
using LuminaGed.Domain.Entities;
using MediatR;

namespace LuminaGed.Application.Features.DocumentsFeatures.Queries.AllDocuments
{
    public class GetAllDocumentsQueryHandler : IRequestHandler<GetAllDocQuery, List<DocumentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<DocumentEntity> _documentRepository;

        public GetAllDocumentsQueryHandler(IMapper mapper, IGenericRepository<DocumentEntity> documentRepository)
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        public async Task<List<DocumentDto>> Handle(GetAllDocQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var documents = await _documentRepository.GetAsync();

                if (documents != null)
                {
                    var documentDtos = _mapper.Map<List<DocumentDto>>(documents);
                    return documentDtos;
                }

                throw new NotFoundException("Pas de documents trouvés.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Une erreur s'est produite lors de la récupération des documents : {ex.Message}");
            }
        }
    }
}
