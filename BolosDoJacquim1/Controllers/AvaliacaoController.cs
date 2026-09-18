using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacao _avaliacaoRepository;

        public AvaliacaoController(IAvaliacao avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Avaliacao> avaliacoes = await _avaliacaoRepository.Listar();
                return Ok(avaliacoes);
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
                Avaliacao? avaliacao = await _avaliacaoRepository.BuscarPorId(id);

                if (avaliacao == null)
                {
                    return NotFound("Avaliação não encontrada.");
                }

                return Ok(avaliacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("produto/{idProduto}")]
        public async Task<IActionResult> GetByProduto(Guid idProduto)
        {
            try
            {
                List<Avaliacao> avaliacoes = await _avaliacaoRepository.ListarPorProduto(idProduto);
                return Ok(avaliacoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AvaliacaoDTO dto)
        {
            try
            {
                var novaAvaliacao = new Avaliacao
                {
                    Nota = dto.Nota,
                    Comentario = dto.Comentario,
                    IdProduto = dto.IdProduto,
                    IdUsuario = dto.IdUsuario,
                    DataCriacao = DateTime.Now
                };

                await _avaliacaoRepository.Cadastrar(novaAvaliacao);

                return StatusCode(201, novaAvaliacao);
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
                await _avaliacaoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}