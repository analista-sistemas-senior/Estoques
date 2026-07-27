namespace Estoques.Service.DTOs
{
    public record FornecedorDTO(int IDFornecedor, int IDUsuario, string NMFornecedor, string? TXEndereco, string? TXAnotacao);
}