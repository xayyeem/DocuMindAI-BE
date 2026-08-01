using StorageService.Application.Features.DTO.Documents;
using StorageService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Features.Interfaces
{
    public interface IDocumentService
    {
        Task<Result<UploadDocumentResponse>> uploadAsync(UploadDocumentRequest request, CancellationToken cancellation = default);
    }
}
