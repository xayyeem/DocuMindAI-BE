using StorageService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Features.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task AddAsync(Document document, CancellationToken cancellationToken = default);

        Task UpdateAsync(Document document, CancellationToken cancellationToken = default);

        Task DeleteAsync(Document document, CancellationToken cancellationToken = default);
    }

}
