using Estoques.Service.DTOs.Relatorio;

namespace Estoques.Service.Interfaces
{
    public interface IRelatorioService
    {
        Task<RelatorioTotalDTO> RetornarRelatorioTotal(int idUsuario);
        Task<List<RelatorioProdutoTipoDTO>> RetornarRelatorioProdutoPorTipo(int idUsuario);
        Task<List<RelatorioProdutoFabricanteDTO>> RetornarRelatorioProdutoPorFabricante(int idUsuario);
        Task<List<RelatorioProdutoFornecedorDTO>> RetornarRelatorioProdutoPorFornecedor(int idUsuario);
        Task<List<RelatorioProdutoCorDTO>> RetornarRelatorioProdutoPorCor(int idUsuario);
    }
}