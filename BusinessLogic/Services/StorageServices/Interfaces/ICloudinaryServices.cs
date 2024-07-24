using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;

namespace BusinessLogic.Services.StorageServices.Interfaces
{
    public interface ICloudinaryServices
    {
        Task<ImageUploadResult> UploadImageAsync(Stream fileStream, string fileName);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
