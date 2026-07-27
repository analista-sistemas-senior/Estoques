using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDTO?> RetornarProdutoPorId(int idProduto);
        Task<ProdutoDTO?> RetornarProdutoPorIdEIdUsuario(int idProduto, int idUsuario);
        Task<List<ProdutoDTO>> RetornarProdutosPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoDTO>> CadastrarProduto(ProdutoDTO produto);
        Task<Resultado<ProdutoDTO>> AtualizarProduto(ProdutoDTO produto);
        Task<Resultado<bool>> ExcluirProduto(int idProduto, int idUsuario);
        Task<Resultado<ProdutoDTO>> RetornarProdutoAutentico(int idProduto, int idUsuario);
    }
}