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
    public class ProdutoController : ControllerBase
    {
        private readonly IProduto _produtoRepository;

        public ProdutoController(IProduto produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Produto> produtos = await _produtoRepository.Listar();
                return Ok(produtos);
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
                Produto? produto = await _produtoRepository.BuscarPorId(id);

                if (produto == null)
                {
                    return NotFound("Produto não encontrado.");
                }

                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDTO dto)
        {
            try
            {
                var novoProduto = new Produto
                {
                    Nome = dto.Nome,
                    Preco = dto.Preco,
                    EnderecoImagem = dto.EnderecoImagem,
                    Descricao = dto.Descricao,
                    Disponibilidade = dto.Disponibilidade,
                    IdCategoria = dto.IdCategoria
                };

                await _produtoRepository.Cadastrar(novoProduto);

                return StatusCode(201, novoProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    mensagem = ex.Message,
                    innerException = ex.InnerException?.Message,
                    detalhes = ex.ToString()
                });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProdutoDTO dto)
        {
            try
            {
                var produtoAtualizado = new Produto
                {
                    Nome = dto.Nome,
                    Preco = dto.Preco,
                    EnderecoImagem = dto.EnderecoImagem,
                    Descricao = dto.Descricao,
                    Disponibilidade = dto.Disponibilidade,
                    IdCategoria = dto.IdCategoria
                };

                await _produtoRepository.Atualizar(id, produtoAtualizado);

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
                await _produtoRepository.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}