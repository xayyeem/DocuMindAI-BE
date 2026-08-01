using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Features.DTO.Documents
{
    public sealed class UploadDocumentRequest
    {
        public Guid UserId { get; set; }
        public Stream FileStream { get; set; } = default!;

        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }
    }
}
