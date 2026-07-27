using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IProdutoTipoRepository
    {
        Task<ProdutoTipo?> RetornarProdutoTipoPorId(int idProdutoTipo);
        Task<ProdutoTipo?> RetornarProdutoTipoPorIdEIdUsuario(int idProdutoTipo, int idUsuario);
        Task<List<ProdutoTipo>> RetornarProdutosTiposPorIdUsuario(int idUsuario);
        Task<ProdutoTipo> CadastrarProdutoTipo(ProdutoTipo produtoTipo);
        Task<ProdutoTipo> AtualizarProdutoTipo(ProdutoTipo produtoTipo);
        Task<bool> ExcluirProdutoTipo(int idProdutoTipo, int idUsuario);
    }
}