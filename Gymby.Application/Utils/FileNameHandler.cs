using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Gymby.Application.Utils
{
    public static class FileNameHandler
    {
        public async static Task<IFormFile> RenameFile(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Guid.NewGuid().ToString(),
                DispositionType = "attachment"
            };

            var formFile = new FormFile(ms, 0, ms.Length, file.Name, Guid.NewGuid().ToString());

            formFile.Headers = new HeaderDictionary();
            // Set the custom Content-Disposition header
            formFile.Headers["Content-Disposition"] = contentDisposition.ToString();
            formFile.Headers["Content-Type"] = file.ContentType;

            ms.Position = 0;

            return formFile;
        } 
    }
}
