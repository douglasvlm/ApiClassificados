using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClassificados.Exceptions
{
    public class ClassificadoNaoCadastradoException : Exception
    {
        public ClassificadoNaoCadastradoException()
           :base("Este classificado não está cadastrado")
        { }

    }
}
