using Estoques.API.Controllers.Base;
using Estoques.Service.DTOs.Usuario;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    public class UsuarioController(IUsuarioService usuarioService, IValidator<UsuarioEntradaDTO> validador) : ControladorBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly IValidator<UsuarioEntradaDTO> _validador = validador;

        [HttpPost("usuarios")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioEntradaDTO usuarioDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(usuarioDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var usuarioResultado = await _usuarioService.CadastrarUsuario(usuarioDTO);
            if (!usuarioResultado.Sucesso) return BadRequest(new { mensagem = usuarioResultado.MensagemErro });

            return CreatedAtAction(nameof(CadastrarUsuario), new { id = usuarioResultado.Dados!.IDUsuario }, usuarioResultado.Dados);
        }

        [Authorize]
        [HttpPut("usuarios")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioEntradaDTO usuarioDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(usuarioDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            if (usuarioDTO.IDUsuario != IDUsuarioLogado) return BadRequest(new { mensagem = "Não atualizado" });

            var usuarioResultado = await _usuarioService.AtualizarUsuario(usuarioDTO);
            if (!usuarioResultado.Sucesso) return NotFound(new { mensagem = usuarioResultado.MensagemErro });

            return NoContent();
        }

        [Authorize]
        [HttpGet("usuarios/perfil")]
        public async Task<ActionResult<UsuarioSaidaDTO>> RetornarPerfilUsuarioLogado()
        {
            var usuario = await _usuarioService.RetornarUsuarioPorId(IDUsuarioLogado);

            if (usuario == null) return NotFound(new { mensagem = "Não encontrado" });

            return Ok(usuario);
        }
    }
}