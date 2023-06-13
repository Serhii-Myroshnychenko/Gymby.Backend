namespace Gymby.UnitTests.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {
        }

        public async Task AddPhotoAsync(string containerName, string userId, string folderName, IFormFile file, string newFileName)
        {
            
        }

        public async Task DeletePhotoAsync(string containerName, string userId, string folderName)
        {

        }

        public async Task DeleteSelectedPhoto(string containerName, string userId, string folderName, string fileName)
        {
            
        }

        public Task<List<string>> GetListOfPhotos(string containerName, string userId, string folderName)
        {
            List<string> photos = new List<string>();
            photos.Add("1");
            return Task.FromResult(photos);
        }

        public Task<string> GetPhotoAsync(string containerName, string userId, string folderName, string fileName)
        {
            return Task.FromResult("2");
        }
    }
}
