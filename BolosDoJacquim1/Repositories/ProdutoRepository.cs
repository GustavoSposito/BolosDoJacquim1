using BolosDoJacquim1.BdContextEvent;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class ProdutoRepository : IProduto
    {
        private readonly EventContext _context;

        public ProdutoRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Cadastrar(Produto produto)
        {
            await _context.Set<Produto>().AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Produto>> Listar()
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> BuscarPorId(Guid id)
        {
            return await _context.Produto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProduto == id);
        }

        public async Task Atualizar(Guid id, Produto produto)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                produtoBuscado.Nome = produto.Nome;
                produtoBuscado.Preco = produto.Preco;
                produtoBuscado.EnderecoImagem = produto.EnderecoImagem;
                produtoBuscado.Descricao = produto.Descricao;
                produtoBuscado.Disponibilidade = produto.Disponibilidade;
                produtoBuscado.IdCategoria = produto.IdCategoria;

                _context.Produto.Update(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(Guid id)
        {
            var produtoBuscado = await _context.Produto.FindAsync(id);

            if (produtoBuscado != null)
            {
                _context.Produto.Remove(produtoBuscado);
                await _context.SaveChangesAsync();
            }
        }
    }
}