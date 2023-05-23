using Microsoft.AspNetCore.Http;

namespace Gymby.Application.Interfaces
{
    public interface IFileService
    {
        Task AddPhotoAsync(string containerName, string userId, string folderName, IFormFile file, string newFileName);
        Task DeletePhotoAsync(string containerName, string userId, string folderName);
        Task DeleteSelectedPhoto(string containerName, string userId, string folderName, string fileName);
        Task<string> GetPhotoAsync(string containerName, string userId, string folderName, string fileName);
        Task<List<string>> GetListOfPhotos(string containerName, string userId, string folderName);
    }
}
