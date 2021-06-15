using System;

namespace ApiClassificados.Entities
{
    public class Classificado
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
    }
}
