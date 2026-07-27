using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoHistoricoRepository
    {
        Task<ProdutoHistorico?> RetornarProdutoHistoricoPorId(int idProdutoHistorico);
        Task<ProdutoHistorico?> RetornarProdutoHistoricoPorIdEIdUsuario(int idProdutoHistorico, int idUsuario);
        Task<List<ProdutoHistorico>> RetornarProdutosHistoricosPorIdUsuario(int idUsuario);
        Task<List<ProdutoHistorico>> RetornarProdutosHistoricosPorIdProdutoEIdUsuario(int idProduto, int idUsuario);
        Task<ProdutoHistorico> CadastrarProdutoHistorico(ProdutoHistorico produtoHistorico);
        Task<ProdutoHistorico> AtualizarProdutoHistorico(ProdutoHistorico produtoHistorico);
        Task<bool> ExcluirProdutoHistorico(int idProdutoHistorico, int idUsuario);
    }
}