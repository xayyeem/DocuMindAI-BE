using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using StorageService.Application.Features.Interfaces;
using StorageService.Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Storage
{
    public class LocalDocumentStorage: IDocumentStorage
    {
        private readonly StorageOptions _options;
        public LocalDocumentStorage(IOptions<StorageOptions> options)
        {
            _options = options.Value;
        }
        public async Task<string> SaveAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
        {
            Directory.CreateDirectory(_options.UploadPath);
            string extension = Path.GetExtension(fileName);
            string storedFileName = $"{Guid.NewGuid()}{extension}";
            string fullPath = Path.Combine(_options.UploadPath, storedFileName);
            await using (var fileStreamOutput = new FileStream(fullPath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput, cancellationToken);
            }
            return storedFileName;
        }

        public Task<Stream> GetAsync(string storedFileName, CancellationToken cancellationToken = default)
        {
            string fullPath = Path.Combine(_options.UploadPath, storedFileName);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File '{storedFileName}' not found.");
            }
            Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return Task.FromResult(stream);
        }

        public Task DeleteAsync(string storedFileName, CancellationToken cancellationToken = default)
        {
            string fullPath = Path.Combine(_options.UploadPath, storedFileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return Task.CompletedTask;
        }
    }
}
