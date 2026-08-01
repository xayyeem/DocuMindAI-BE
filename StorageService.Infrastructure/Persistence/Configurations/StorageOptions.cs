using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Infrastructure.Persistence.Configurations
{
    public sealed class StorageOptions
    {
        public const string SectionName = "Storage";

        public string UploadPath { get; set; } = "Uploads";
    }
}
