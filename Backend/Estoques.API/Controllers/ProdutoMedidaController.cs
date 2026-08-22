using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoMedidaController(IProdutoMedidaService produtoMedidaService, IValidator<ProdutoMedidaDTO> validador) : ControladorBase
    {
        private readonly IProdutoMedidaService _produtoMedidaService = produtoMedidaService;
        private readonly IValidator<ProdutoMedidaDTO> _validador = validador;

        [HttpGet("produtos/medidas")]
        public async Task<ActionResult<IEnumerable<ProdutoMedidaDTO>>> RetornarProdutosMedidasPorIdUsuario()
        {
            var produtoMedidas = await _produtoMedidaService.RetornarProdutosMedidasPorIdUsuario(IDUsuarioLogado);
            return produtoMedidas == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoMedidas);
        }

        [HttpPost("produtos/medidas")]
        public async Task<IActionResult> CadastrarProdutoMedida([FromBody] ProdutoMedidaDTO produtoMedidaDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoMedidaDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoMedida = produtoMedidaDTO with { IDUsuario = IDUsuarioLogado };

            var produtoMedidaResultado = await _produtoMedidaService.CadastrarProdutoMedida(produtoMedida);
            if (!produtoMedidaResultado.Sucesso) return BadRequest(new { mensagem = produtoMedidaResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarProdutoMedida), new { id = produtoMedidaResultado.Dados!.IDProdutoMedida }, produtoMedidaResultado.Dados);
        }

        [HttpPut("produtos/medidas")]
        public async Task<IActionResult> AtualizarProdutoMedida([FromBody] ProdutoMedidaDTO produtoMedidaDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoMedidaDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoMedida = produtoMedidaDTO with { IDUsuario = IDUsuarioLogado };

            var produtoMedidaResultado = await _produtoMedidaService.AtualizarProdutoMedida(produtoMedida);
            if (!produtoMedidaResultado.Sucesso) return NotFound(new { mensagem = produtoMedidaResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("produtos/medidas/{id:int}")]
        public async Task<IActionResult> ExcluirProdutoMedida(int id)
        {
            var produtoMedidaResultado = await _produtoMedidaService.ExcluirProdutoMedida(id, IDUsuarioLogado);
            if (!produtoMedidaResultado.Sucesso) return NotFound(new { mensagem = produtoMedidaResultado.MensagemErro });

            return NoContent();
        }
    }
}