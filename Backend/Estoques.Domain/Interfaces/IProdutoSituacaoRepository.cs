using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoSituacaoRepository
    {
        Task<ProdutoSituacao?> RetornarProdutoSituacaoPorId(int idProdutoSituacao);
        Task<ProdutoSituacao?> RetornarProdutoSituacaoPorIdEIdUsuario(int idProdutoSituacao, int idUsuario);
        Task<List<ProdutoSituacao>> RetornarProdutosSituacoesPorIdUsuario(int idUsuario);
        Task<ProdutoSituacao> CadastrarProdutoSituacao(ProdutoSituacao produtoSituacao);
        Task<ProdutoSituacao> AtualizarProdutoSituacao(ProdutoSituacao produtoSituacao);
        Task<bool> ExcluirProdutoSituacao(int idProdutoSituacao, int idUsuario);
    }
}