﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gymby.UnitTests.Services
{
    public class FileService : IFileService
    {
        public FileService()
        {
        }
        public async Task AddPhotoAsync(string containerName, string userId, string folderName, IFormFile file)
        {
            
        }
        public async Task DeletePhotoAsync(string containerName, string userId, string folderName)
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