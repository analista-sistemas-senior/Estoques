using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoFabricanteController(IProdutoFabricanteService produtoFabricanteService, IValidator<ProdutoFabricanteDTO> validador) : ControladorBase
    {
        private readonly IProdutoFabricanteService _produtoFabricanteService = produtoFabricanteService;
        private readonly IValidator<ProdutoFabricanteDTO> _validador = validador;

        [HttpGet("produtos/fabricantes")]
        public async Task<ActionResult<IEnumerable<ProdutoFabricanteDTO>>> RetornarProdutosFabricantesPorIdUsuario()
        {
            var produtoFabricantes = await _produtoFabricanteService.RetornarProdutosFabricantesPorIdUsuario(IDUsuarioLogado);
            return produtoFabricantes == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtoFabricantes);
        }

        [HttpPost("produtos/fabricantes")]
        public async Task<IActionResult> CadastrarProdutoFabricante([FromBody] ProdutoFabricanteDTO produtoFabricanteDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoFabricanteDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoFabricante = produtoFabricanteDTO with { IDUsuario = IDUsuarioLogado };

            var produtoFabricanteResultado = await _produtoFabricanteService.CadastrarProdutoFabricante(produtoFabricante);
            if (!produtoFabricanteResultado.Sucesso) return BadRequest(new { mensagem = produtoFabricanteResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarProdutoFabricante), new { id = produtoFabricanteResultado.Dados!.IDProdutoFabricante }, produtoFabricanteResultado.Dados);
        }

        [HttpPut("produtos/fabricantes")]
        public async Task<IActionResult> AtualizarProdutoFabricante([FromBody] ProdutoFabricanteDTO produtoFabricanteDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoFabricanteDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var produtoFabricante = produtoFabricanteDTO with { IDUsuario = IDUsuarioLogado };

            var produtoFabricanteResultado = await _produtoFabricanteService.AtualizarProdutoFabricante(produtoFabricante);
            if (!produtoFabricanteResultado.Sucesso) return NotFound(new { mensagem = produtoFabricanteResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("produtos/fabricantes/{id:int}")]
        public async Task<IActionResult> ExcluirProdutoFabricante(int id)
        {
            var produtoFabricanteResultado = await _produtoFabricanteService.ExcluirProdutoFabricante(id, IDUsuarioLogado);
            if (!produtoFabricanteResultado.Sucesso) return NotFound(new { mensagem = produtoFabricanteResultado.MensagemErro });

            return NoContent();
        }
    }
}