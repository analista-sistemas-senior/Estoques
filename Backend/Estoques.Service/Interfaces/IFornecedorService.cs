using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IFornecedorService
    {
        Task<FornecedorDTO?> RetornarFornecedorPorId(int idFornecedor);
        Task<FornecedorDTO?> RetornarFornecedorPorIdEIdUsuario(int idFornecedor, int idUsuario);
        Task<List<FornecedorDTO>> RetornarFornecedoresPorIdUsuario(int idUsuario);
        Task<Resultado<FornecedorDTO>> CadastrarFornecedor(FornecedorDTO fornecedor);
        Task<Resultado<FornecedorDTO>> AtualizarFornecedor(FornecedorDTO fornecedor);
        Task<Resultado<bool>> ExcluirFornecedor(int idFornecedor, int idUsuario);
        Task<Resultado<FornecedorDTO>> RetornarFornecedorAutentico(int idFornecedor, int idUsuario);
    }
}