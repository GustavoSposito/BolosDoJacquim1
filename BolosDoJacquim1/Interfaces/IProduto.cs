using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BolosDoJacquin.Models; // Ajuste para .Domains se necessário

namespace BolosDoJacquin.Interfaces
{
    public interface IProduto
    {
        Task Cadastrar(Produto produto);

        Task<List<Produto>> Listar();

        Task<Produto?> BuscarPorId(Guid id);

        Task Atualizar(Guid id, Produto produto);

        Task Deletar(Guid id);
    }
}