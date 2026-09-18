using BolosDoJacquim1.BdContextEvent;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models; // Ajuste se usar .Domains
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        private readonly EventContext _context; // Ajuste para o nome exato do seu DbContext

        public CategoriaRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Cadastrar(Categoria categoria)
        {
            await _context.Set<Categoria>().AddAsync(categoria);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria?> BuscarPorId(Guid id)
        {
            return await _context.Categoria.FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task Atualizar(Guid id, Categoria categoria)
        {
            var categoriaBuscada = await _context.Categoria.FindAsync(id);

            if (categoriaBuscada != null)
            {
                categoriaBuscada.Nome = categoria.Nome;
                _context.Categoria.Update(categoriaBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(Guid id)
        {
            var categoriaBuscada = await _context.Categoria.FindAsync(id);

            if (categoriaBuscada != null)
            {
                _context.Categoria.Remove(categoriaBuscada);
                await _context.SaveChangesAsync();
            }
        }
    }
}