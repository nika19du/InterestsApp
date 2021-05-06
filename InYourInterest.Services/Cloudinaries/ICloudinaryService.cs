using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InYourInterest.Services.Cloudinaries
{
    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(IFormFile pictureFile, string fileName);
    }
}
