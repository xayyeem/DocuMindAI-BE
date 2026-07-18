using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Common
{
    public abstract class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

        public Guid? CreatedBy { get; protected set; }

        public DateTime? UpdatedAt { get; protected set; }

        public Guid? UpdatedBy { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime? DeletedAt { get; protected set; }

        public Guid? DeletedBy { get; protected set; }

        public void MarkAsCreated(Guid? userId = null)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userId;
        }

        public void MarkAsUpdated(Guid? userId = null)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }

        public void SoftDelete(Guid? userId = null)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }

        public void Restore(Guid? userId = null)
        {
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;

            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = userId;
        }
    }
}
