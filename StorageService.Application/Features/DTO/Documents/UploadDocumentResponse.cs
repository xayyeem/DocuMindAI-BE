using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Features.DTO.Documents
{
    public sealed class UploadDocumentResponse
    {
        public Guid DocumentId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
