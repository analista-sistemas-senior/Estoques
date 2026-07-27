using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class ProdutoFabricanteMapping
{
    public static ProdutoFabricanteDTO ParaDTO(this ProdutoFabricante produtoFabricante)
    {
        return new ProdutoFabricanteDTO(produtoFabricante.IDProdutoFabricante, produtoFabricante.IDUsuario, produtoFabricante.NMProdutoFabricante);
    }

    public static List<ProdutoFabricanteDTO> ParaDTOs(this List<ProdutoFabricante> produtoFabricantes)
    {
        return [.. produtoFabricantes.Select(pf => pf.ParaDTO()).ToList()];
    }

    public static ProdutoFabricante ParaEntidade(this ProdutoFabricanteDTO produtoFabricanteDTO)
    {
        return new ProdutoFabricante(produtoFabricanteDTO.IDProdutoFabricante, produtoFabricanteDTO.IDUsuario, produtoFabricanteDTO.NMProdutoFabricante);
    }
}