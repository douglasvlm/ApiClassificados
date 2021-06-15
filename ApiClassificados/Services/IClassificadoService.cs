using ApiClassificados.InputModel;
using ApiClassificados.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClassificados.Services
{
    public interface IClassificadoService : IDisposable
    {
        Task<List<ClassificadoViewModel>> Obter(int pagina, int quantidade);
        Task<ClassificadoViewModel> Obter(Guid id);
        Task<ClassificadoViewModel> Inserir(ClassificadoInputModel classificado);
        Task Atualizar(Guid id, ClassificadoInputModel classificado);
        Task Atualizar(Guid id, double valor);
        Task Remover(Guid id);

    }
}
