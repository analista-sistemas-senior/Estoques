using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoMedidaService
    {
        Task<ProdutoMedidaDTO?> RetornarProdutoMedidaPorId(int idProdutoMedida);
        Task<ProdutoMedidaDTO?> RetornarProdutoMedidaPorIdEIdUsuario(int idProdutoMedida, int idUsuario);
        Task<List<ProdutoMedidaDTO>> RetornarProdutosMedidasPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoMedidaDTO>> CadastrarProdutoMedida(ProdutoMedidaDTO produtoMedida);
        Task<Resultado<ProdutoMedidaDTO>> AtualizarProdutoMedida(ProdutoMedidaDTO produtoMedida);
        Task<Resultado<bool>> ExcluirProdutoMedida(int idProdutoMedida, int idUsuario);
        Task<Resultado<ProdutoMedidaDTO>> RetornarProdutoMedidaAutentico(int idProdutoMedida, int idUsuario);
    }
}