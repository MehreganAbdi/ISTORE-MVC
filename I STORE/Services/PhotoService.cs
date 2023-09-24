using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using I_STORE.Interfaces;
using I_STORE.PhotoCloudThing;
using Microsoft.Extensions.Options;

namespace I_STORE.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary; 
        public PhotoService(IOptions<CloudinarySetup> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File= new FileDescription(file.FileName , stream),
                    Transformation = new Transformation().Height(300).Width(300).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var res = await _cloudinary.DestroyAsync(deleteParams);
            return res;
        }
    }
}
