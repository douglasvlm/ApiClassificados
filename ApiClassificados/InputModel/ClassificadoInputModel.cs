using System;
using System.ComponentModel.DataAnnotations;

namespace ApiClassificados.InputModel
{
    public class ClassificadoInputModel
    {
    
        
        [Display(Name = "Título")]
        [StringLength(200, MinimumLength = 3, ErrorMessage ="O título do classificado deve conter de 3 a 200 caracteres.")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(4000, MinimumLength = 10, ErrorMessage = "A descrição do classificado deve conter de 10 a 4000 caracteres.")]
        public string Descricao { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Range(1, 1000000, ErrorMessage = "O valor deve ser de no mínimo 1 real e no máximo 1000000")]
        public double Valor { get; set; }
    }
}
