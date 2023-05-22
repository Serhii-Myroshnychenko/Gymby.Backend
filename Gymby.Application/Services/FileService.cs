using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Gymby.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Gymby.Application.Services;

public class FileService : IFileService
{
    private readonly BlobServiceClient _blobServiceClient;

    public FileService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    public async Task AddPhotoAsync(string containerName, string userId, string folderName, IFormFile file)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        string folderPath = $"{userId}/{folderName}/{file.FileName}";

        BlobClient folderBlobClient = containerClient.GetBlobClient(folderPath);

        await folderBlobClient.UploadAsync(file.OpenReadStream());
    }
    public async Task DeletePhotoAsync(string containerName, string userId, string folderName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        string folderPath = $"{userId}/{folderName}";

        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync(prefix: folderPath))
        {
            if (blobItem is BlobItem blob)
            {
                BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
                await blobClient.DeleteIfExistsAsync();
            }
        }
    }

    public Task<List<string>> GetListOfPhotos(string containerName, string userId, string folderName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetPhotoAsync(string containerName, string userId, string folderName, string fileName)
    {
        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        string folderPath = $"{userId}/{folderName}/{fileName}";

        BlobClient folderBlobClient = containerClient.GetBlobClient(folderPath);

        if(await folderBlobClient.ExistsAsync())
        {
            return folderBlobClient.Uri.ToString();
        }

        return null;
    }
}