using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoSituacaoService
    {
        Task<ProdutoSituacaoDTO?> RetornarProdutoSituacaoPorId(int idProdutoSituacao);
        Task<ProdutoSituacaoDTO?> RetornarProdutoSituacaoPorIdEIdUsuario(int idProdutoSituacao, int idUsuario);
        Task<List<ProdutoSituacaoDTO>> RetornarProdutoSituacaosPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoSituacaoDTO>> CadastrarProdutoSituacao(ProdutoSituacaoDTO produtoSituacao);
        Task<Resultado<ProdutoSituacaoDTO>> AtualizarProdutoSituacao(ProdutoSituacaoDTO produtoSituacao);
        Task<Resultado<bool>> ExcluirProdutoSituacao(int idProdutoSituacao, int idUsuario);
        Task<Resultado<ProdutoSituacaoDTO>> RetornarProdutoSituacaoAutentico(int idProdutoSituacao, int idUsuario);
    }
}