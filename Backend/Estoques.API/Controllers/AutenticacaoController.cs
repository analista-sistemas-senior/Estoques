using Estoques.API.Controllers.Base;
using Estoques.API.Services;
using Estoques.Service.DTOs.Autenticacao;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Estoques.API.Controllers
{
    [Route("api/autenticacao")]
    public class AutenticacaoController(IUsuarioService usuarioService, ITokenService tokenService, IValidator<AutenticacaoEntradaDTO> validador) : ControladorBase
    {
        private readonly IUsuarioService _usuarioService = usuarioService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IValidator<AutenticacaoEntradaDTO> _validador = validador;

        [HttpPost("login")]
        public async Task<ActionResult<AutenticacaoSaidaDTO>> Login([FromBody] AutenticacaoEntradaDTO loginDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(loginDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            var usuario = await _usuarioService.AutenticarUsuario(loginDTO.NMLogin, loginDTO.CDSenha);

            if (!usuario.Sucesso) return Unauthorized(new AutenticacaoSaidaDTO(0, null!, null!, null!, usuario.MensagemErro));

            return Ok(new AutenticacaoSaidaDTO(usuario.Dados!.IDUsuario, usuario.Dados.NMUsuario, usuario.Dados.NMLogin, _tokenService.GerarToken(usuario.Dados)));
        }
    }
}