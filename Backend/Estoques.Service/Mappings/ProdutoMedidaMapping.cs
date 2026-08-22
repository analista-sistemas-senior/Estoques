using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class ProdutoMedidaMapping
{
    public static ProdutoMedidaDTO ParaDTO(this ProdutoMedida produtoMedida)
    {
        return new ProdutoMedidaDTO(produtoMedida.IDProdutoMedida, produtoMedida.IDUsuario, produtoMedida.MDProdutoMedida);
    }

    public static List<ProdutoMedidaDTO> ParaDTOs(this List<ProdutoMedida> produtoMedidas)
    {
        return [.. produtoMedidas.Select(pf => pf.ParaDTO()).ToList()];
    }

    public static ProdutoMedida ParaEntidade(this ProdutoMedidaDTO produtoMedidaDTO)
    {
        return new ProdutoMedida(produtoMedidaDTO.IDProdutoMedida, produtoMedidaDTO.IDUsuario, produtoMedidaDTO.MDProdutoMedida);
    }
}