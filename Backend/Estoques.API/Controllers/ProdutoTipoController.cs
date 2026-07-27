using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoTipoController(IProdutoTipoService produtoTipoService, IValidator<ProdutoTipoDTO> validador) : ControladorBase
    {
        private readonly IProdutoTipoService _produtoTipoService = produtoTipoService;
        private readonly IValidator<ProdutoTipoDTO> _validador = validador;

        [HttpGet("produtos/tipos")]
        public async Task<ActionResult<IEnumerable<ProdutoTipoDTO>>> RetornarProdutoTiposPorIdUsuario()
        {
            var produtoTipos = await _produtoTipoService.RetornarProdutoTiposPorIdUsuario(IDUsuarioLogado);
            return produtoTipos == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoTipos);
        }

        [HttpPost("produtos/tipos")]
        public async Task<IActionResult> CadastrarProdutoTipo([FromBody] ProdutoTipoDTO produtoTipoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoTipoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoTipo = produtoTipoDTO with { IDUsuario = IDUsuarioLogado };

            var produtoTipoResultado = await _produtoTipoService.CadastrarProdutoTipo(produtoTipo);
            if (!produtoTipoResultado.Sucesso) return BadRequest(new { mensagem = produtoTipoResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarProdutoTipo), new { id = produtoTipoResultado.Dados!.IDProdutoTipo }, produtoTipoResultado.Dados);
        }

        [HttpPut("produtos/tipos")]
        public async Task<IActionResult> AtualizarProdutoTipo([FromBody] ProdutoTipoDTO produtoTipoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoTipoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoTipo = produtoTipoDTO with { IDUsuario = IDUsuarioLogado };

            var produtoTipoResultado = await _produtoTipoService.AtualizarProdutoTipo(produtoTipo);
            if (!produtoTipoResultado.Sucesso) return NotFound(new { mensagem = produtoTipoResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("produtos/tipos/{id:int}")]
        public async Task<IActionResult> ExcluirProdutoTipo(int id)
        {
            var produtoTipoResultado = await _produtoTipoService.ExcluirProdutoTipo(id, IDUsuarioLogado);
            if (!produtoTipoResultado.Sucesso) return NotFound(new { mensagem = produtoTipoResultado.MensagemErro });

            return NoContent();
        }
    }
}