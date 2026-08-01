namespace StorageService.Domain.Constants
{
    public static class FileConstants
    {
        // 20 MB
        public const long MaxFileSize = 20 * 1024 * 1024;

        public static readonly string[] AllowedExtensions =
        {
            ".pdf"
        };

        public static readonly string[] AllowedContentTypes =
        {
            "application/pdf"
        };
    }
}