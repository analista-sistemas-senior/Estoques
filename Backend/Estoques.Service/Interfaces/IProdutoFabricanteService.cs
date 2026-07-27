using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IProdutoFabricanteService
    {
        Task<ProdutoFabricanteDTO?> RetornarProdutoFabricantePorId(int idProdutoFabricante);
        Task<ProdutoFabricanteDTO?> RetornarProdutoFabricantePorIdEIdUsuario(int idProdutoFabricante, int idUsuario);
        Task<List<ProdutoFabricanteDTO>> RetornarProdutosFabricantesPorIdUsuario(int idUsuario);
        Task<Resultado<ProdutoFabricanteDTO>> CadastrarProdutoFabricante(ProdutoFabricanteDTO produtoFabricante);
        Task<Resultado<ProdutoFabricanteDTO>> AtualizarProdutoFabricante(ProdutoFabricanteDTO produtoFabricante);
        Task<Resultado<bool>> ExcluirProdutoFabricante(int idProdutoFabricante, int idUsuario);
        Task<Resultado<ProdutoFabricanteDTO>> RetornarProdutoFabricanteAutentico(int idProdutoFabricante, int idUsuario);
    }
}