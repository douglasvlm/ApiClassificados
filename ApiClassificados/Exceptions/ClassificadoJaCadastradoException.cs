using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClassificados.Exceptions
{
    public class ClassificadoJaCadastradoException : Exception
    {
        public ClassificadoJaCadastradoException()
           : base("Este classificado já está cadastrado")
        { }
    }
}
