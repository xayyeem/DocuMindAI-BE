using Microsoft.AspNetCore.Mvc;
using StorageService.Application.Features.DTO.Documents;
using StorageService.Application.Features.Interfaces;
using StorageService.Application.Services;
using System.Threading;

namespace StorageService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController:ControllerBase
    {
        private readonly IDocumentService _documentServices;
        public DocumentsController(IDocumentService documentService)
        {
            _documentServices = documentService;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellation = default)
        {
            if (file == null)
            {
                return BadRequest("No file provided.");
            }
            var request = new UploadDocumentRequest
            {
                UserId = Guid.NewGuid(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                FileStream = file.OpenReadStream()
            };
            var result = await _documentServices.uploadAsync(
                request,
                cancellation);

            if (result.IsFailure)
            {
                return BadRequest(new
                {
                    result.Error.Code,
                    result.Error.Message
                });
            }
            return Ok(result.Value);
        }
    }
}
