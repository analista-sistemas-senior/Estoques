namespace Estoques.Service.DTOs
{
    public record AdquirenteDTO(int IDAdquirente, int IDUsuario, string NMAdquirente, string? TXEndereco, string? TXAnotacao);
}