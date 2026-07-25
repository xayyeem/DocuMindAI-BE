using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Application.Features.Interfaces
{
    public interface IDocumentStorage
    {
        Task<string> SaveAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);

        Task<Stream> GetAsync(string storedFileName, CancellationToken cancellationToken = default);

        Task DeleteAsync(string storedFileName, CancellationToken cancellationToken = default);
    }
}
