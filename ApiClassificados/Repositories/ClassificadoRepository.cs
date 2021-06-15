using ApiClassificados.Entities;
using ApiClassificados.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExemploApiCatalogoJogos.Repositories
{
    public class ClassificadoRepository : IClassificadoRepository
    {
        private static Dictionary<Guid, Classificado> classificados = new Dictionary<Guid, Classificado>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Classificado{ Id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Titulo = "Vende-se Carro", Descricao = "Porsche 911 3.0 Carrera 4S Coupe 2020", Valor = 899900} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Classificado{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Titulo = "Aluga-se Casa", Descricao = "Mansão 540m², 5 suites frente mar vizinado ao hotel e restaurante. Valor tarifa média/noite.", Valor = 190} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Classificado{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Titulo = "Vende-se IPhone", Descricao = "iPhone 12 Pro Apple 256GB Grafite 6,1” - Câm. Tripla 12MP iOS", Valor = 7900} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Classificado{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Titulo = "Contrata-se estágiario de TI", Descricao = "Estágio - Programação ASP.NET", Valor = 1500} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Classificado{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Titulo = "Manutenção de Hardware", Descricao = "TARIFAS PARA COMPUTADORES MAC - Min R$95,00 Os preços são estimativas aproximadas.", Valor = 150} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Classificado{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Titulo = "Procuro estágio em TI", Descricao = "Objetivo: - Atuar no setor de TI e adiquirir experencia profissional.", Valor = 190} }
        };

        public Task<List<Classificado>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(classificados.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Classificado> Obter(Guid id)
        {
            if (!classificados.ContainsKey(id))
                return Task.FromResult<Classificado>(null);

            return Task.FromResult(classificados[id]);
        }

        public Task<List<Classificado>> Obter(string titulo, string descricao)
        {
            return Task.FromResult(classificados.Values.Where(classificado => classificado.Titulo.Equals(titulo) && classificado.Descricao.Equals(descricao)).ToList());
        }

        public Task<List<Classificado>> ObterSemLambda(string titulo, string descricao)
        {
            var retorno = new List<Classificado>();

            foreach (var classificado in classificados.Values)
            {
                if (classificado.Titulo.Equals(titulo) && classificado.Descricao.Equals(descricao))
                    retorno.Add(classificado);
            }

            return Task.FromResult(retorno);
        }

        public Task Inserir(Classificado classificado)
        {
            classificados.Add(classificado.Id, classificado);
            return Task.CompletedTask;
        }

        public Task Atualizar(Classificado classificado)
        {
            classificados[classificado.Id] = classificado;
            return Task.CompletedTask;
        }

        public Task Remover(Guid id)
        {
            classificados.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco
        }
    }
}
