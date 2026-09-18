using BolosDoJacquim1.BdContextEvent;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;   // Ajuste para o namespace das suas Models
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
        private readonly EventContext _context;

        public TipoUsuarioRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            var tipoUsuarioBuscado = await _context.TipoUsuario.FindAsync(id);

            if (tipoUsuarioBuscado != null)
            {
                tipoUsuarioBuscado.TituloTipoUsuario = tipoUsuario.TituloTipoUsuario;
                _context.TipoUsuario.Update(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoUsuario?> BuscarPorId(Guid id)
        {
            return await _context.TipoUsuario.FirstOrDefaultAsync(t => t.IdTipoUsuario == id);
        }

        public async Task Cadastrar(TipoUsuario tipoUsuario)
        {
            await _context.Set<TipoUsuario>().AddAsync(tipoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoUsuarioBuscado = await _context.TipoUsuario.FindAsync(id);

            if (tipoUsuarioBuscado != null)
            {
                _context.TipoUsuario.Remove(tipoUsuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<TipoUsuario>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}