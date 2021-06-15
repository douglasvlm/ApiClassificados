using ApiClassificados.Entities;
using ApiClassificados.Exceptions;
using ApiClassificados.InputModel;
using ApiClassificados.Repositories;
using ApiClassificados.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClassificados.Services
{
    public class ClassificadoService : IClassificadoService
    {
        private readonly IClassificadoRepository _classificadoRepository;

        public ClassificadoService(IClassificadoRepository classificadoRepository)
        {
            _classificadoRepository = classificadoRepository;
        }

        public async Task<List<ClassificadoViewModel>> Obter(int pagina, int quantidade)
        {
            var classificados = await _classificadoRepository.Obter(pagina, quantidade);

            return classificados.Select(classificado => new ClassificadoViewModel
            {
                Id = classificado.Id,
                Titulo = classificado.Titulo,
                Descricao = classificado.Descricao,
                Valor = classificado.Valor
            })
                               .ToList();
        }

        public async Task<ClassificadoViewModel> Obter(Guid id)
        {
            var classificado = await _classificadoRepository.Obter(id);

            if (classificado == null)
                return null;

            return new ClassificadoViewModel
            {
                Id = classificado.Id,
                Titulo = classificado.Titulo,
                Descricao = classificado.Descricao,
                Valor = classificado.Valor
            };
        }

        public async Task<ClassificadoViewModel> Inserir(ClassificadoInputModel classificado)
        {
            var entidadeClassificado = await _classificadoRepository.Obter(classificado.Titulo, classificado.Descricao);

            if (entidadeClassificado.Count > 0)
                throw new ClassificadoJaCadastradoException();

            var classificadoInsert = new Classificado
            {
                Id = Guid.NewGuid(),
                Titulo = classificado.Titulo,
                Descricao = classificado.Descricao,
                Valor = classificado.Valor
            };

            await _classificadoRepository.Inserir(classificadoInsert);

            return new ClassificadoViewModel
            {
                Id = classificadoInsert.Id,
                Titulo = classificado.Titulo,
                Descricao = classificado.Descricao,
                Valor = classificado.Valor
            };
        }

        public async Task Atualizar(Guid id, ClassificadoInputModel classificado)
        {
            var entidadeClassificado = await _classificadoRepository.Obter(id);

            if (entidadeClassificado == null)
                throw new ClassificadoNaoCadastradoException();

            entidadeClassificado.Titulo = classificado.Titulo;
            entidadeClassificado.Descricao = classificado.Descricao;
            entidadeClassificado.Valor = classificado.Valor;

            await _classificadoRepository.Atualizar(entidadeClassificado);
        }

        public async Task Atualizar(Guid id, double valor)
        {
            var entidadeClassificado = await _classificadoRepository.Obter(id);

            if (entidadeClassificado == null)
                throw new ClassificadoNaoCadastradoException();

            entidadeClassificado.Valor = valor;

            await _classificadoRepository.Atualizar(entidadeClassificado);
        }

        public async Task Remover(Guid id)
        {
            var classificado = await _classificadoRepository.Obter(id);

            if (classificado == null)
                throw new ClassificadoNaoCadastradoException();

            await _classificadoRepository.Remover(id);
        }

       
        public void Dispose()
        {
            _classificadoRepository?.Dispose();
        }
    }
}
