using BolosDoJacquim1.BdContextEvent;
using BolosDoJacquin.Interfaces;
using BolosDoJacquin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BolosDoJacquin.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly EventContext _context;

        public UsuarioRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Cadastrar(Usuario usuario)
        {
            // Opcional: Se vocês usam BCrypt no projeto anterior para criptografar a senha:
            // usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _context.Usuario
                .Include(u => u.IdTipoUsuario)
                .ToListAsync();
        }

        public async Task<Usuario?> BuscarPorId(Guid id)
        {
            return await _context.Usuario
                .Include(u => u.IdTipoUsuario)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<Usuario?> BuscarPorEmailESenha(string email, string senha)
        {
            return await _context.Usuario
                .Include(u => u.IdTipoUsuario)
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        public async Task Atualizar(Guid id, Usuario usuario)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Nome = usuario.Nome;
                usuarioBuscado.Email = usuario.Email;

                if (!string.IsNullOrEmpty(usuario.Senha))
                {
                    usuarioBuscado.Senha = usuario.Senha;
                }

                usuarioBuscado.IdTipoUsuario = usuario.IdTipoUsuario;

                _context.Usuario.Update(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(Guid id)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                _context.Usuario.Remove(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }
    }
}