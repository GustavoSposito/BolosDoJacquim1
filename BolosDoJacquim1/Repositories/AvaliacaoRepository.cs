using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



using BolosDoJacquim1.BdContextEvent;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;

namespace BolosDoJacquin.Repositories
{
    public class AvaliacaoRepository : IAvaliacao
    {
        private readonly EventContext _context;

        public AvaliacaoRepository(EventContext context)
        {
            _context = context;
        }

      

        public async Task<List<Avaliacao>> Listar()
        {
            return await _context.Avaliacao
                .Include(a => a.Produto)
                .Include(a => a.Usuario)
                .ToListAsync();
        }

        public async Task<Avaliacao?> BuscarPorId(Guid id)
        {
            return await _context.Avaliacao
                .Include(a => a.Produto)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(a => a.IdAvaliacao == id);
        }

        public async Task<List<Avaliacao>> ListarPorProduto(Guid idProduto)
        {
            return await _context.Avaliacao
                .Include(a => a.Usuario)
                .Where(a => a.IdProduto == idProduto)
                .ToListAsync();
        }

        public async Task Deletar(Guid id)
        {
            var avaliacaoBuscada = await _context.Avaliacao.FindAsync(id);

            if (avaliacaoBuscada != null)
            {
                _context.Avaliacao.Remove(avaliacaoBuscada);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Cadastrar(Guid id, Avaliacao avaliacao)
        {
            await _context.Set<Avaliacao>().AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public Task<Avaliacao> Cadastrar(Avaliacao avaliacao)
        {
            throw new NotImplementedException();
        }
    }
}