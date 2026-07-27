using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoHistoricoService
    {
        Task<ProdutoHistoricoDTO?> RetornarProdutoHistoricoPorId(int idProdutoHistorico);
        Task<ProdutoHistoricoDTO?> RetornarProdutoHistoricoPorIdEIdUsuario(int idProdutoHistorico, int idUsuario);
        Task<List<ProdutoHistoricoDTO>> RetornarProdutosHistoricosPorIdUsuario(int idUsuario);
        Task<List<ProdutoHistoricoDTO>> RetornarProdutosHistoricosPorIdProdutoEIdUsuario(int idProduto, int idUsuario);
        Task<Resultado<ProdutoHistoricoDTO>> CadastrarProdutoHistorico(ProdutoHistoricoDTO produtoHistorico);
        Task<Resultado<ProdutoHistoricoDTO>> AtualizarProdutoHistorico(ProdutoHistoricoDTO produtoHistorico);
        Task<Resultado<bool>> ExcluirProdutoHistorico(int idProdutoHistorico, int idUsuario);
        Task<Resultado<ProdutoHistoricoDTO>> RetornarProdutoHistoricoAutentico(int idProdutoHistorico, int idUsuario);
    }
}