using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Utils;

namespace BolosDoJacquin.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> options)
        {
            // Desempacotamento das configs (CloudName, ApiKey, ApiSecret)
            var credenciais = options.Value;

            // Account com as credenciais da conta do Cloudinary
            var account = new Account(credenciais.CloudName, credenciais.ApiKey, credenciais.ApiSecret);

            // Criar o cliente autenticado
            _cloudinary = new Cloudinary(account);

            // Garante que as URLs venham com HTTPS
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImagem(IFormFile arquivo)
        {
            // Abre o fluxo de leitura do arquivo
            using var stream = arquivo.OpenReadStream();

            // Monta os parâmetros do upload
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(arquivo.FileName, stream),
                Folder = "bolosdojacquin/produtos"
            };

            // Envia a imagem para o Cloudinary
            var resultado = await _cloudinary.UploadAsync(uploadParams);

            // Retorna a URL segura
            return resultado.SecureUrl.AbsoluteUri;
        }
    }
}