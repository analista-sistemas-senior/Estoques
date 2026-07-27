using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IFornecedorRepository
    {
        Task<Fornecedor?> RetornarFornecedorPorId(int idFornecedor);
        Task<Fornecedor?> RetornarFornecedorPorIdEIdUsuario(int idFornecedor, int idUsuario);
        Task<List<Fornecedor>> RetornarFornecedoresPorIdUsuario(int idUsuario);
        Task<Fornecedor> CadastrarFornecedor(Fornecedor fornecedor);
        Task<Fornecedor> AtualizarFornecedor(Fornecedor fornecedor);
        Task<bool> ExcluirFornecedor(int idFornecedor, int idUsuario);
    }
}