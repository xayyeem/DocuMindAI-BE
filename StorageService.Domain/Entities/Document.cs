using StorageService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string OriginalFileName { get; private set; } = string.Empty;
        public string StoredFileName { get; private set; } = string.Empty;

        public string ContentType { get; private set; } = string.Empty;

        public long FileSize { get; private set; }

        public DocumentStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        private Document()
        {
            // Required by EF Core
        }
        public Document(Guid userId, string originalName, string storedFileName, string contentType, long fileSize)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            OriginalFileName = originalName;
            StoredFileName = storedFileName;
            ContentType = contentType;
            FileSize = fileSize;
            Status = DocumentStatus.Uploaded;
            CreatedAt = DateTime.UtcNow;
        }

        public void MarkAsProcessing()
        {
            Status = DocumentStatus.Processing;
        }
        public void MarkAsCompleted()
        {
            Status = DocumentStatus.Completed;
        }
        public void MarkAsFailed()
        {
            Status = DocumentStatus.Failed;
        }
    }
}
