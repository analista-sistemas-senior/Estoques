using Estoques.Domain.Entities;
using Estoques.Service.DTOs.Usuario;

namespace Estoques.Service.Mappings;

public static class UsuarioMapping
{
    public static UsuarioSaidaDTO ParaSaidaDTO(this Usuario usuario)
    {
        return new UsuarioSaidaDTO(usuario.IDUsuario, usuario.NMUsuario, usuario.NMLogin);
    }

    public static List<UsuarioSaidaDTO> ParaSaidasDTOs(this List<Usuario> usuarios)
    {
        return [.. usuarios.Select(u => u.ParaSaidaDTO())];
    }

    public static Usuario ParaEntidade(this UsuarioEntradaDTO usuarioDTO)
    {
        return new Usuario(usuarioDTO.IDUsuario, usuarioDTO.NMUsuario, usuarioDTO.NMLogin, usuarioDTO.CDSenha);
    }
}