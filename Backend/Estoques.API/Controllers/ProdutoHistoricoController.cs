using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoHistoricoController(IProdutoHistoricoService produtoHistoricoService, IValidator<ProdutoHistoricoDTO> validador) : ControladorBase
    {
        private readonly IProdutoHistoricoService _produtoHistoricoService = produtoHistoricoService;
        private readonly IValidator<ProdutoHistoricoDTO> _validador = validador;

        [HttpGet("produtos/historicos")]
        public async Task<ActionResult<IEnumerable<ProdutoHistoricoDTO>>> RetornarProdutoHistoricosPorIdUsuario()
        {
            var produtoHistoricos = await _produtoHistoricoService.RetornarProdutosHistoricosPorIdUsuario(IDUsuarioLogado);
            return produtoHistoricos == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoHistoricos);
        }

        [HttpGet("produtos/historicos/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProdutoHistoricoDTO>>> RetornarProdutoHistoricosPorIdProdutoEIdUsuario(int id)
        {
            var produtoHistoricos = await _produtoHistoricoService.RetornarProdutosHistoricosPorIdProdutoEIdUsuario(id, IDUsuarioLogado);
            return produtoHistoricos == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoHistoricos);
        }

        [HttpPost("produtos/historicos")]
        public async Task<IActionResult> CadastrarProdutoHistorico([FromBody] ProdutoHistoricoDTO produtoHistoricoDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new { mensagem = "Não cadastrado" });

            var validacaoDTO = await _validador.ValidateAsync(produtoHistoricoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            try 
            { 
                var produtoHistoricoResultado = await _produtoHistoricoService.CadastrarProdutoHistorico(produtoHistoricoDTO);
                if (!produtoHistoricoResultado.Sucesso) return BadRequest(new { mensagem = produtoHistoricoResultado.MensagemErro });
                return CreatedAtAction(nameof(CadastrarProdutoHistorico), new { id = produtoHistoricoResultado.Dados!.IDProdutoHistorico }, produtoHistoricoResultado.Dados);
            }
            catch (DbUpdateException) { return BadRequest(new { mensagem = "Não cadastrado" }); }
        }

        [HttpPut("produtos/historicos")]
        public async Task<IActionResult> AtualizarProdutoHistorico([FromBody] ProdutoHistoricoDTO produtoHistoricoDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new { mensagem = "Não atualizado" });

            var validacaoDTO = await _validador.ValidateAsync(produtoHistoricoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            try
            {
                var produtoHistoricoResultado = await _produtoHistoricoService.AtualizarProdutoHistorico(produtoHistoricoDTO);
                if (!produtoHistoricoResultado.Sucesso) return NotFound(new { mensagem = produtoHistoricoResultado.MensagemErro });
                return NoContent();
            }
            catch (DbUpdateException) { return BadRequest(new { mensagem = "Não atualizado" }); }
        }

        [HttpDelete("produtos/historicos/{id:int}")]
        public async Task<IActionResult> ExcluirProdutoHistorico(int id)
        {
            var produtoHistoricoResultado = await _produtoHistoricoService.ExcluirProdutoHistorico(id, IDUsuarioLogado);
            if (!produtoHistoricoResultado.Sucesso) return NotFound(new { mensagem = produtoHistoricoResultado.MensagemErro });

            return NoContent();
        }
    }
}