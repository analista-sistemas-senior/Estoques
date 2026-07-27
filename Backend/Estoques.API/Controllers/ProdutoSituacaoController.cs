using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoSituacaoController(IProdutoSituacaoService produtoSituacaoService, IValidator<ProdutoSituacaoDTO> validador) : ControladorBase
    {
        private readonly IProdutoSituacaoService _produtoSituacaoService = produtoSituacaoService;
        private readonly IValidator<ProdutoSituacaoDTO> _validador = validador;

        [HttpGet("produtos/situacoes")]
        public async Task<ActionResult<IEnumerable<ProdutoSituacaoDTO>>> RetornarProdutoSituacaosPorIdUsuario()
        {
            var produtoSituacaos = await _produtoSituacaoService.RetornarProdutoSituacaosPorIdUsuario(IDUsuarioLogado);
            return produtoSituacaos == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoSituacaos);
        }

        [HttpPost("produtos/situacoes")]
        public async Task<IActionResult> CadastrarProdutoSituacao([FromBody] ProdutoSituacaoDTO produtoSituacaoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoSituacaoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoSituacao = produtoSituacaoDTO with { IDUsuario = IDUsuarioLogado };

            var produtoSituacaoResultado = await _produtoSituacaoService.CadastrarProdutoSituacao(produtoSituacao);
            if (!produtoSituacaoResultado.Sucesso) return BadRequest(new { mensagem = produtoSituacaoResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarProdutoSituacao), new { id = produtoSituacaoResultado.Dados!.IDProdutoSituacao }, produtoSituacaoResultado.Dados);
        }

        [HttpPut("produtos/situacoes")]
        public async Task<IActionResult> AtualizarProdutoSituacao([FromBody] ProdutoSituacaoDTO produtoSituacaoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoSituacaoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoSituacao = produtoSituacaoDTO with { IDUsuario = IDUsuarioLogado };

            var produtoSituacaoResultado = await _produtoSituacaoService.AtualizarProdutoSituacao(produtoSituacao);
            if (!produtoSituacaoResultado.Sucesso) return NotFound(new { mensagem = produtoSituacaoResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("produtos/situacoes/{id:int}")]
        public async Task<IActionResult> ExcluirProdutoSituacao(int id)
        {
            var produtoSituacaoResultado = await _produtoSituacaoService.ExcluirProdutoSituacao(id, IDUsuarioLogado);
            if (!produtoSituacaoResultado.Sucesso) return NotFound(new { mensagem = produtoSituacaoResultado.MensagemErro });

            return NoContent();
        }
    }
}