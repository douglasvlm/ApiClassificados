using ApiClassificados.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApiClassificados.Repositories
{
    public class ClassificadoSQLServerRepository : IClassificadoRepository
    {
        private readonly SqlConnection sqlConnection;

        public ClassificadoSQLServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Classificado>> Obter(int pagina, int quantidade)
        {
            var classificados = new List<Classificado>();

            var comando = $"select * from Classificados order by Id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                classificados.Add(new Classificado
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Titulo = (string)sqlDataReader["Titulo"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Valor = (double)sqlDataReader["Valor"]
                });
            }

            await sqlConnection.CloseAsync();

            return classificados;
        }

        public async Task<Classificado> Obter(Guid id)
        {
            Classificado classificado = null;

            var comando = $"select * from Classificados where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                classificado = new Classificado
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Titulo = (string)sqlDataReader["Titulo"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Valor = (double)sqlDataReader["Valor"]
                };
            }

            await sqlConnection.CloseAsync();

            return classificado;
        }

        public async Task<List<Classificado>> Obter(string titulo, string descricao)
        {
            var classificados = new List<Classificado>();

            var comando = $"select * from Classificados where Titulo = '{titulo}' and Descricao = '{descricao}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                classificados.Add(new Classificado
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Titulo = (string)sqlDataReader["Titulo"],
                    Descricao = (string)sqlDataReader["Descricao"],
                    Valor = (double)sqlDataReader["Valor"]
                });
            }

            await sqlConnection.CloseAsync();

            return classificados;
        }

        public async Task Inserir(Classificado classificado)
        {
            var comando = $"insert Classificados (Id, Titulo, Descricao, Valor) values ('{classificado.Id}', '{classificado.Titulo}', '{classificado.Descricao}', {classificado.Valor.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Classificado classificado)
        {
            var comando = $"update Classificados set Titulo = '{classificado.Titulo}', Descricao = '{classificado.Descricao}', Valor = {classificado.Valor.ToString().Replace(",", ".")} where Id = '{classificado.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Classificados where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
