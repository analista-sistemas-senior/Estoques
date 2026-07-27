using Estoques.Service.DTOs.Usuario;

namespace Estoques.API.Services
{
    public interface ITokenService
    {
        string GerarToken(UsuarioSaidaDTO usuario);
    }
}
