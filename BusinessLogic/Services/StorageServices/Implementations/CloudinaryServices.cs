using Microsoft.Extensions.Options;
using BusinessLogic.Services.StorageServices.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;
using BusinessLogic.Infrastructure.Cloudinary;

namespace BusinessLogic.Services.StorageServices.Implementations
{
    public class CloudinaryServices : ICloudinaryServices
    {
        private readonly ICloudinary _cloudinary;
        public CloudinaryServices(ICloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public CloudinaryServices(IOptions<CloudinarySettings> config, ILogger<CloudinaryServices> logger)
        {
            logger.LogInformation($"CloudName: {config.Value.CloudName}");
            logger.LogInformation($"ApiKey: {config.Value.ApiKey}");
            // Don't log ApiSecret in production as it's sensitive information

            if (string.IsNullOrEmpty(config.Value.CloudName) ||
                string.IsNullOrEmpty(config.Value.ApiKey) ||
                string.IsNullOrEmpty(config.Value.ApiSecret))
            {
                throw new ArgumentException("Cloudinary settings are not configured properly.");
            }
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result =await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
        

        public async Task<ImageUploadResult> UploadImageAsync(Stream fileStream, string fileName)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult;
        }
    }
}