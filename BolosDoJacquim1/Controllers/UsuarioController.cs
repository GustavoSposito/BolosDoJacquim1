
using Microsoft.AspNetCore.Mvc;
using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Usuario> usuarios = await _usuarioRepository.Listar();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                Usuario? usuario = await _usuarioRepository.BuscarPorId(id);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDTO dto)
        {
            try
            {
                var novoUsuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    IdTipoUsuario = dto.IdTipoUsuario,
                    DataCadastro = DateTime.Now
                };

                await _usuarioRepository.Cadastrar(novoUsuario);

                return StatusCode(201, novoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UsuarioDTO dto)
        {
            try
            {
                var usuarioAtualizado = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    IdTipoUsuario = dto.IdTipoUsuario
                };

                await _usuarioRepository.Atualizar(id, usuarioAtualizado);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _usuarioRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}