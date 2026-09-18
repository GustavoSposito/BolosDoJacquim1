using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;

namespace BolosDoJacquin.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação de usuários via JWT (JSON Web Token).
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuario _usuario;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuario usuario, IConfiguration configuration)
        {
            _usuario = usuario;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            // 1. Busca o usuário pelo e-mail e valida a senha com BCrypt
            var usuarioEncontrado = await _usuario.BuscarPorEmailESenha(dto.Email, dto.Senha);

            // 2. Se as credenciais forem inválidas, retorna 401 Unauthorized
            if (usuarioEncontrado == null)
            {
                return Unauthorized("Email ou senha inválidos!");
            }

            // 3. Criar a lista de Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioEncontrado.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.Email),
                new Claim("nome", usuarioEncontrado.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // 4. Criar a chave de segurança com base no appsettings.json
            var chaveSecreta = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            // 5. Definir o algoritmo de assinatura
            var credenciais = new SigningCredentials(chaveSecreta, SecurityAlgorithms.HmacSha256);

            // 6. Montar o token JWT
            var token = new JwtSecurityToken(
                issuer: "BolosDoJacquin.WebAPI",
                audience: "BolosDoJacquin.WebAPI",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            // 7. Retornar a resposta estruturada
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                Expiracao = token.ValidTo,
                Usuario = new
                {
                    usuarioEncontrado.IdUsuario,
                    usuarioEncontrado.Nome,
                    usuarioEncontrado.Email
                }
            });
        }
    }
}