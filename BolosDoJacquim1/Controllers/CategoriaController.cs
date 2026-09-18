using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BolosDoJacquin.DTOs;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models; // Ajuste para .Domains se a sua model estiver na pasta Domains

namespace BolosDoJacquin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoria _categoriaRepository;

        public CategoriaController(ICategoria categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Categoria> lista = await _categoriaRepository.Listar();
                return Ok(lista);
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
                Categoria? categoriaBuscada = await _categoriaRepository.BuscarPorId(id);

                if (categoriaBuscada == null)
                {
                    return NotFound("Categoria não encontrada.");
                }

                return Ok(categoriaBuscada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoriaDTO dto)
        {
            try
            {
                var novaCategoria = new Categoria
                {
                    Nome = dto.Nome
                };

                await _categoriaRepository.Cadastrar(novaCategoria);

                return StatusCode(201, novaCategoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, CategoriaDTO dto)
        {
            try
            {
                var categoriaAtualizada = new Categoria
                {
                    Nome = dto.Nome
                };

                await _categoriaRepository.Atualizar(id, categoriaAtualizada);

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
                await _categoriaRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}