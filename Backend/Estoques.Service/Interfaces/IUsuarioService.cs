using Estoques.Service.Common;
using Estoques.Service.DTOs.Usuario;

namespace Estoques.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioSaidaDTO?> RetornarUsuarioPorId(int idUsuario);
        Task<UsuarioSaidaDTO?> RetornarUsuarioPorLogin(string nmLogin);
        Task<Resultado<UsuarioSaidaDTO>> CadastrarUsuario(UsuarioEntradaDTO usuario);
        Task<Resultado<UsuarioSaidaDTO>> AtualizarUsuario(UsuarioEntradaDTO usuario);
        Task<Resultado<UsuarioSaidaDTO>> AutenticarUsuario(string nmLogin, string cdSenha);
    }
}