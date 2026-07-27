using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> RetornarUsuarioPorId(int idUsuario);
        Task<Usuario?> RetornarUsuarioPorLogin(string nmLogin);
        Task<Usuario> CadastrarUsuario(Usuario usuario);
        Task<Usuario> AtualizarUsuario(Usuario usuario);
    }
}