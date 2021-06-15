using ApiClassificados.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClassificados.Repositories
{
    public interface IClassificadoRepository : IDisposable
    {
        Task<List<Classificado>> Obter(int pagina, int quantidade);
        Task<Classificado> Obter(Guid id);
        Task<List<Classificado>> Obter(string titulo, string descricao);
        Task Inserir(Classificado classificado);
        Task Atualizar(Classificado classificado);
        Task Remover(Guid id);

    }
}
