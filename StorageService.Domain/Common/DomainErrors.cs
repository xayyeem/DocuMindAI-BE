using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageService.Domain.Common
{
    public static class DomainErrors
    {
        public static class Document
        {
            public static readonly Error EmptyFile =
                new(
                    "Document.EmptyFile",
                    "Uploaded file is empty.");

            public static readonly Error InvalidFileType =
                new(
                    "Document.InvalidFileType",
                    "Only PDF files are allowed.");

            public static readonly Error FileTooLarge =
                new(
                    "Document.FileTooLarge",
                    "The uploaded file exceeds the maximum allowed size.");

            public static readonly Error NotFound =
                new(
                    "Document.NotFound",
                    "Document not found.");

            public static readonly Error StorageFailed =
                new(
                    "Document.StorageFailed",
                    "Failed to store the document.");

            public static readonly Error AlreadyProcessing =
                new(
                    "Document.AlreadyProcessing",
                    "Document is already being processed.");
        }
    }
}
