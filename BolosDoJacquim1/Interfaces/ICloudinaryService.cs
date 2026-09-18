using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BolosDoJacquin.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImagem(IFormFile arquivo);
    }
}