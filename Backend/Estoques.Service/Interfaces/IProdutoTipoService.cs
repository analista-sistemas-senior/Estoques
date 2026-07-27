using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoTipoService
    {
        Task<ProdutoTipoDTO?> RetornarProdutoTipoPorId(int idProdutoTipo);
        Task<ProdutoTipoDTO?> RetornarProdutoTipoPorIdEIdUsuario(int idProdutoTipo, int idUsuario);
        Task<List<ProdutoTipoDTO>> RetornarProdutoTiposPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoTipoDTO>> CadastrarProdutoTipo(ProdutoTipoDTO produtoTipo);
        Task<Resultado<ProdutoTipoDTO>> AtualizarProdutoTipo(ProdutoTipoDTO produtoTipo);
        Task<Resultado<bool>> ExcluirProdutoTipo(int idProdutoTipo, int idUsuario);
        Task<Resultado<ProdutoTipoDTO>> RetornarProdutoTipoAutentico(int idProdutoTipo, int idUsuario);
    }
}