using Estoques.Service.Common;
using Estoques.Service.DTOs.Produto;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoSaidaDTO?> RetornarProdutoPorId(int idProduto);
        Task<ProdutoSaidaDTO?> RetornarProdutoPorIdEIdUsuario(int idProduto, int idUsuario);
        Task<List<ProdutoSaidaDTO>> RetornarProdutosPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoSaidaDTO>> CadastrarProduto(ProdutoEntradaDTO produto);
        Task<Resultado<ProdutoSaidaDTO>> AtualizarProduto(ProdutoEntradaDTO produto);
        Task<Resultado<bool>> ExcluirProduto(int idProduto, int idUsuario);
        Task<Resultado<ProdutoSaidaDTO>> RetornarProdutoAutentico(int idProduto, int idUsuario);
    }
}