using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BolosDoJacquin.Models;

namespace BolosDoJacquin.Interfaces
{
    public interface IAvaliacao
    {
        Task<Avaliacao> Cadastrar(Avaliacao avaliacao);

        Task<List<Avaliacao>> Listar();

        Task<Avaliacao?> BuscarPorId(Guid id);

        Task<List<Avaliacao>> ListarPorProduto(Guid idProduto);

        Task Deletar(Guid id);
    }
}