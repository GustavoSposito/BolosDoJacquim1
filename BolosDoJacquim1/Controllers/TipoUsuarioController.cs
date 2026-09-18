using BolosDoJacquin.DTO;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {

        private readonly ITipoUsuario _tipoUsuario;
        public TipoUsuarioController(ITipoUsuario tipoUsuario)
        {
            _tipoUsuario = tipoUsuario;
        }

        [HttpGet("{id:guid}")]

        public async Task<IActionResult> BuscarPorId(Guid id)
        {
            var tipoUsuarioBuscando = await
                _tipoUsuario.BuscarPorId(id);

            if (tipoUsuarioBuscando == null)
            {
                return NotFound("Tipo de usuario não encontrado.");
            }

            return Ok(tipoUsuarioBuscando);
        }


        /// <summary>
        /// Lista todos os perfis de usuario cadastrados no sistema.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _tipoUsuario.Listar();

                return Ok(tipos);
            }
            catch (Exception erro)
            {

                return BadRequest(erro.Message);
            }
        }

        /// <summary>
        /// <param name="tipoUsuario"> perfil do usuario a ser cadastrado</param>
        /// </summary>
        /// <param name="tipoUsuario"></param>
        /// <returns></returns>
        [HttpPost]

        // from body apartir do corpo eu vou passar as informaçoes/ essas informaçoes vao ser passada no from body

        public async Task<IActionResult> Cadastrar([FromBody] TipoUsuarioDTO dto)
        {
            var tipoUsuario = new TipoUsuario
            {
                TituloTipoUsuario = dto.Titulo
            };


            await _tipoUsuario.Cadastrar(tipoUsuario);

            //return CreatedAtAction("BuscarPorId", new { id = tipoUsuario.IdTipoUsuario }, tipoUsuario);

            return StatusCode(201, tipoUsuario);

        }

        [HttpPut("{id:Guid}")]

        public async Task<IActionResult> Atualizar(Guid id, [FromBody] TipoUsuarioDTO dto)
        {
            var tipoUsuario = new TipoUsuario
            {
                TituloTipoUsuario = dto.Titulo
            };

            await _tipoUsuario.Atualizar(id, tipoUsuario);

            return Ok(tipoUsuario);

        }

        /// <summary>
        /// remove um perfil de usuariopelo id
        /// </summary>
        /// <param name="id">Id do perfil a ser removido</param>
        /// <returns></returns>

        [HttpDelete("{id:Guid}")]

        public async Task<IActionResult> Deletar(Guid id)
        {
            await _tipoUsuario.Deletar(id);
            return NoContent();
        }


    }
}
