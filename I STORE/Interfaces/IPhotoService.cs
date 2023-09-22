using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;

namespace I_STORE.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
