using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produto?> RetornarProdutoPorId(int idProduto);
        Task<Produto?> RetornarProdutoPorIdEIdUsuario(int idProduto, int idUsuario);
        Task<List<Produto>> RetornarProdutosPorIdUsuario(int idUsuario);
        Task<Produto> CadastrarProduto(Produto produto);
        Task<Produto> AtualizarProduto(Produto produto);
        Task<bool> ExcluirProduto(int idProduto, int idUsuario);
    }
}