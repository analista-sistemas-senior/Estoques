using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class FornecedorController(IFornecedorService fornecedorService, IValidator<FornecedorDTO> validador) : ControladorBase
    {
        private readonly IFornecedorService _fornecedorService = fornecedorService;
        private readonly IValidator<FornecedorDTO> _validador = validador;

        [HttpGet("fornecedores")]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> RetornarFornecedoresPorIdUsuario()
        {
            var fornecedors = await _fornecedorService.RetornarFornecedoresPorIdUsuario(IDUsuarioLogado);
            return fornecedors == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(fornecedors);
        }

        [HttpPost("fornecedores")]
        public async Task<IActionResult> CadastrarFornecedor([FromBody] FornecedorDTO fornecedorDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(fornecedorDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var fornecedor = fornecedorDTO with { IDUsuario = IDUsuarioLogado };

            var fornecedorResultado = await _fornecedorService.CadastrarFornecedor(fornecedor);
            if (!fornecedorResultado.Sucesso) return BadRequest(new { mensagem = fornecedorResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarFornecedor), new { id = fornecedorResultado.Dados!.IDFornecedor }, fornecedorResultado.Dados);
        }

        [HttpPut("fornecedores")]
        public async Task<IActionResult> AtualizarFornecedor([FromBody] FornecedorDTO fornecedorDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(fornecedorDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var fornecedor = fornecedorDTO with { IDUsuario = IDUsuarioLogado };

            var fornecedorResultado = await _fornecedorService.AtualizarFornecedor(fornecedor);
            if (!fornecedorResultado.Sucesso) return NotFound(new { mensagem = fornecedorResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("fornecedores/{id:int}")]
        public async Task<IActionResult> ExcluirFornecedor(int id)
        {
            var fornecedorResultado = await _fornecedorService.ExcluirFornecedor(id, IDUsuarioLogado);
            if (!fornecedorResultado.Sucesso) return NotFound(new { mensagem = fornecedorResultado.MensagemErro });

            return NoContent();
        }
    }
}