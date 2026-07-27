using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs.Usuario;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public async Task<UsuarioSaidaDTO?> RetornarUsuarioPorId(int idUsuario)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorId(idUsuario);
            return usuario?.ParaSaidaDTO();
        }

        public async Task<UsuarioSaidaDTO?> RetornarUsuarioPorLogin(string nmLogin)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorLogin(nmLogin);
            return usuario?.ParaSaidaDTO();
        }

        public async Task<Resultado<UsuarioSaidaDTO>> CadastrarUsuario(UsuarioEntradaDTO usuario)
        {
            var usuarioExistente = await _usuarioRepository.RetornarUsuarioPorLogin(usuario.NMLogin);
            if (usuarioExistente != null) return Resultado<UsuarioSaidaDTO>.Falha("Nome de login já existente");

            var usuarioNovo = usuario.ParaEntidade();
            string senhaCriptografada = _passwordHasher.HashPassword(usuarioNovo, usuarioNovo.CDSenha);
            usuarioNovo.DefinirSenhaCriptografada(senhaCriptografada);

            var usuarioCadastrado = await _usuarioRepository.CadastrarUsuario(usuarioNovo);
            if (usuarioCadastrado == null) return Resultado<UsuarioSaidaDTO>.Falha("Não cadastrado");

            return Resultado<UsuarioSaidaDTO>.Ok(usuarioCadastrado.ParaSaidaDTO());
        }

        public async Task<Resultado<UsuarioSaidaDTO>> AtualizarUsuario(UsuarioEntradaDTO usuario)
        {
            try
            {
                var usuarioAtualizado = usuario.ParaEntidade();
                usuarioAtualizado.DefinirSenhaCriptografada(_passwordHasher.HashPassword(usuarioAtualizado, usuarioAtualizado.CDSenha));

                await _usuarioRepository.AtualizarUsuario(usuarioAtualizado);
                return Resultado<UsuarioSaidaDTO>.Ok(usuarioAtualizado.ParaSaidaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<UsuarioSaidaDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<UsuarioSaidaDTO>> AutenticarUsuario(string nmLogin, string cdSenha)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorLogin(nmLogin);
            if (usuario == null) return Resultado<UsuarioSaidaDTO>.Falha("Usuário inexistente");

            var verificacaoSenha = _passwordHasher.VerifyHashedPassword(usuario, usuario.CDSenha, cdSenha);
            if (verificacaoSenha == PasswordVerificationResult.Success || verificacaoSenha == PasswordVerificationResult.SuccessRehashNeeded) return Resultado<UsuarioSaidaDTO>.Ok(usuario.ParaSaidaDTO());

            return Resultado<UsuarioSaidaDTO>.Falha("Senha incorreta");
        }
    }
}