using Microsoft.EntityFrameworkCore;
using StorageService.Application.Features.Interfaces;
using StorageService.Domain.Entities;
using StorageService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Document document, CancellationToken cancellationToken = default)
        {
            await _context.Documents.AddAsync(document, cancellationToken);
        }

        public async Task DeleteAsync(Document document, CancellationToken cancellationToken = default)
        {
            _context.Documents.Remove(document);
            await Task.CompletedTask;
        }

        public async Task<Document?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Document document, CancellationToken cancellationToken = default)
        {
            _context.Documents.Update(document);

            return Task.CompletedTask;
        }
    }
}
