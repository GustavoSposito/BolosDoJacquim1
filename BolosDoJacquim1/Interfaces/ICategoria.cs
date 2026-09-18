using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BolosDoJacquin.Models; // Ajuste se usar .Domains

namespace BolosDoJacquin.Interfaces
{
    public interface ICategoria
    {
        Task Cadastrar(Categoria categoria);

        Task<List<Categoria>> Listar();

        Task<Categoria?> BuscarPorId(Guid id);

        Task Atualizar(Guid id, Categoria categoria);

        Task Deletar(Guid id);
    }
}