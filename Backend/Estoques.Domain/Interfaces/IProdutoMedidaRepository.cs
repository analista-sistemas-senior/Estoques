using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoMedidaRepository
    {
        Task<ProdutoMedida?> RetornarProdutoMedidaPorId(int idProdutoMedida);
        Task<ProdutoMedida?> RetornarProdutoMedidaPorIdEIdUsuario(int idProdutoMedida, int idUsuario);
        Task<List<ProdutoMedida>> RetornarProdutosMedidasPorIdUsuario(int idUsuario);
        Task<ProdutoMedida> CadastrarProdutoMedida(ProdutoMedida produtoMedida);
        Task<ProdutoMedida> AtualizarProdutoMedida(ProdutoMedida produtoMedida);
        Task<bool> ExcluirProdutoMedida(int idProdutoMedida, int idUsuario);
    }
}