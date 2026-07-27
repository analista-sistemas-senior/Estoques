namespace Estoques.API.Services
{
    public interface IArmazenamentoService
    {
        Task<string> SalvarArquivo(IFormFile arquivo);
        void ExcluirArquivo(string? arquivoCaminho);
    }
}