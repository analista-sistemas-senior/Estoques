using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoFabricanteRepository
    {
        Task<ProdutoFabricante?> RetornarProdutoFabricantePorId(int idProdutoFabricante);
        Task<ProdutoFabricante?> RetornarProdutoFabricantePorIdEIdUsuario(int idProdutoFabricante, int idUsuario);
        Task<List<ProdutoFabricante>> RetornarProdutosFabricantesPorIdUsuario(int idUsuario);
        Task<ProdutoFabricante> CadastrarProdutoFabricante(ProdutoFabricante produtoFabricante);
        Task<ProdutoFabricante> AtualizarProdutoFabricante(ProdutoFabricante produtoFabricante);
        Task<bool> ExcluirProdutoFabricante(int idProdutoFabricante, int idUsuario);
    }
}