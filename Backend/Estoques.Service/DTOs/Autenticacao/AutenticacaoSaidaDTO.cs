namespace Estoques.Service.DTOs.Autenticacao
{
    public record AutenticacaoSaidaDTO(int IDUsuario, string NMUsuario, string NMLogin, string CDToken, string TXMensagem = "");
}