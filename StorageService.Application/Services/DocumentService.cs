using StorageService.Application.Features.DTO.Documents;
using StorageService.Application.Features.Interfaces;
using StorageService.Domain.Common;
using StorageService.Domain.Constants;
using StorageService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StorageService.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentStorage _documentStorage;
        private readonly IUnitOfWork _unitOfWork;
        public DocumentService( IDocumentRepository documentRepository, IDocumentStorage documentStorage, IUnitOfWork unitOfWork)
        {
            _documentRepository = documentRepository;
            _documentStorage = documentStorage;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UploadDocumentResponse>> uploadAsync(UploadDocumentRequest request, CancellationToken cancellation = default)
        {
            if (request.FileSize <= 0)
            {
                return Result<UploadDocumentResponse>.Failure(DomainErrors.Document.EmptyFile);
            }
            if (request.FileSize > FileConstants.MaxFileSize)
            {
                return Result<UploadDocumentResponse>.Failure(
                    DomainErrors.Document.FileTooLarge);
            }
            var extension = Path.GetExtension(request.FileName).ToLowerInvariant();

            if (!FileConstants.AllowedExtensions.Contains(extension))
            {
                return Result<UploadDocumentResponse>.Failure(
                    DomainErrors.Document.InvalidFileType);
            }

            var storedFileName = await _documentStorage.SaveAsync(
                request.FileStream,
                request.FileName,
                cancellation);

            var document = new Document(
                request.UserId,
                request.FileName,
                storedFileName,
                request.ContentType,
                request.FileSize);

            await _documentRepository.AddAsync(document, cancellation);

            await _unitOfWork.SaveChangesAsync(cancellation);

            // Response
            var response = new UploadDocumentResponse
            {
                DocumentId = document.Id,
                FileName = document.OriginalFileName,
                FileSize = document.FileSize,
                Status = document.Status.ToString()
            };

            return Result<UploadDocumentResponse>.Success(response);

        }
    }
}
