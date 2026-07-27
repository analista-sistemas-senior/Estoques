using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class AdquirenteController(IAdquirenteService adquirenteService, IValidator<AdquirenteDTO> validador) : ControladorBase
    {
        private readonly IAdquirenteService _adquirenteService = adquirenteService;
        private readonly IValidator<AdquirenteDTO> _validador = validador;

        [HttpGet("adquirentes")]
        public async Task<ActionResult<IEnumerable<AdquirenteDTO>>> RetornarAdquirentesPorIdUsuario()
        {
            var adquirentes = await _adquirenteService.RetornarAdquirentesPorIdUsuario(IDUsuarioLogado);
            return adquirentes == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(adquirentes);
        }

        [HttpPost("adquirentes")]
        public async Task<IActionResult> CadastrarAdquirente([FromBody] AdquirenteDTO adquirenteDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(adquirenteDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var adquirente = adquirenteDTO with { IDUsuario = IDUsuarioLogado };

            var adquirenteResultado = await _adquirenteService.CadastrarAdquirente(adquirente);
            if (!adquirenteResultado.Sucesso) return BadRequest(new { mensagem = adquirenteResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarAdquirente), new { id = adquirenteResultado.Dados!.IDAdquirente }, adquirenteResultado.Dados);
        }

        [HttpPut("adquirentes")]
        public async Task<IActionResult> AtualizarAdquirente([FromBody] AdquirenteDTO adquirenteDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(adquirenteDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var adquirente = adquirenteDTO with { IDUsuario = IDUsuarioLogado };

            var adquirenteResultado = await _adquirenteService.AtualizarAdquirente(adquirente);
            if (!adquirenteResultado.Sucesso) return NotFound(new { mensagem = adquirenteResultado.MensagemErro });

            return NoContent();
        }

        [HttpDelete("adquirentes/{id:int}")]
        public async Task<IActionResult> ExcluirAdquirente(int id)
        {
            var adquirenteResultado = await _adquirenteService.ExcluirAdquirente(id, IDUsuarioLogado);
            if (!adquirenteResultado.Sucesso) return NotFound(new { mensagem = adquirenteResultado.MensagemErro });

            return NoContent();
        }
    }
}