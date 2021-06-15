using ApiClassificados.Exceptions;
using ApiClassificados.InputModel;
using ApiClassificados.Services;
using ApiClassificados.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClassificados.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class ClassificadosController : ControllerBase
    {

        private readonly IClassificadoService _classificadoService;

        public ClassificadosController(IClassificadoService classificadoService)
        {
            _classificadoService = classificadoService;
        }

        /// <summary>
        /// Buscar todos os classificados de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar os classificados sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de classificados</response>
        /// <response code="204">Caso não haja classificados</response>   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassificadoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var classificados = await _classificadoService.Obter(pagina, quantidade);

            if (classificados.Count() == 0)
                return NoContent();

            return Ok(classificados);
        }

        /// <summary>
        /// Buscar um classificados pelo seu Id
        /// </summary>
        /// <param name="idClassificado">Id do classificados buscado</param>
        /// <response code="200">Retorna o classificados filtrado</response>
        /// <response code="204">Caso não haja classificados com este id</response>   
        [HttpGet("{idClassificado:guid}")]
        public async Task<ActionResult<ClassificadoViewModel>> Obter([FromRoute] Guid idClassificado)
        {
            var classificado = await _classificadoService.Obter(idClassificado);

            if (classificado == null)
                return NoContent();

            return Ok(classificado);
        }


        /// <summary>
        /// Inserir um classificado
        /// </summary>
        /// <param name="classificadoInputModel">Dados do classificado a ser inserido</param>
        /// <response code="200">Caso o classificado seja inserido com sucesso</response>
        /// <response code="422">Caso já exista um classificado com mesmo título para a mesma descrição</response>   
        [HttpPost]
        public async Task<ActionResult<ClassificadoViewModel>> InserirJogo([FromBody] ClassificadoInputModel classificadoInputModel)
        {
            try
            {
                var classificado = await _classificadoService.Inserir(classificadoInputModel);

                return Ok(classificado);
            }
            //catch(Exception ex)
            catch (ClassificadoJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um classificado com este nome.");
            }
        }

        /// <summary>
        /// Atualizar um classificado
        /// </summary>
        /// /// <param name="idClassificado">Id do classificado a ser atualizado</param>
        /// <param name="classificadoInputModel">Novos dados para atualizar o classificado indicado</param>
        /// <response code="200">Caso o classificado seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um classificado com este Id</response>   
        [HttpPut("{idClassificado:guid}")]
        public async Task<ActionResult> AtualizarClassificado([FromRoute] Guid idClassificado, [FromBody] ClassificadoInputModel classificadoInputModel)
        {
            try
            {
               await _classificadoService.Atualizar(idClassificado, classificadoInputModel);

                return Ok();
            }
            catch (ClassificadoNaoCadastradoException ex)
        //  catch (Exception ex)
            {
                return NotFound("Não existe este classificado");
            }
        }

        /// <summary>
        /// Atualizar o valor de um classificado
        /// </summary>
        /// /// <param name="idClassificado">Id do classificado a ser atualizado</param>
        /// <param name="valor">Novo valor do classificado</param>
        /// <response code="200">Caso o valor seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista um classificado com este Id</response>   
        [HttpPatch("{idClassificado:guid}/valor/{valor:double}")]
        public async Task<ActionResult> AtualizarClassificado([FromRoute] Guid idClassificado, [FromRoute] double valor)
        {
            try
            {
                await _classificadoService.Atualizar(idClassificado, valor);

                return Ok();
            }
            catch (ClassificadoNaoCadastradoException ex)
          //  catch (Exception ex)
            {
                return NotFound("Não existe este classificado");
            }
        }

        /// <summary>
        /// Excluir um classificado
        /// </summary>
        /// /// <param name="idClassificado">Id do classificado a ser excluído</param>
        /// <response code="200">Caso o classificado excluído com sucesso</response>
        /// <response code="404">Caso não exista um classificado com este Id</response>   
        [HttpDelete("{idClassificado:guid}")]
        public async Task<ActionResult> ApagarClassificado([FromRoute] Guid idClassificado)
        {
            try
            {
                await _classificadoService.Remover(idClassificado);

                return Ok();
            }
             catch (ClassificadoNaoCadastradoException ex)
           // catch (Exception ex)
            {
                return NotFound("Não existe este classificado");
            }
        }
    }
}



