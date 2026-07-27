using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class ProdutoTipoMapping
{
    public static ProdutoTipoDTO ParaDTO(this ProdutoTipo produtoTipo)
    {
        return new ProdutoTipoDTO(produtoTipo.IDProdutoTipo, produtoTipo.IDUsuario, produtoTipo.NMProdutoTipo);
    }

    public static List<ProdutoTipoDTO> ParaDTOs(this List<ProdutoTipo> produtoTipos)
    {
        return [.. produtoTipos.Select(pt => pt.ParaDTO()).ToList()];
    }

    public static ProdutoTipo ParaEntidade(this ProdutoTipoDTO produtoTipoDTO)
    {
        return new ProdutoTipo(produtoTipoDTO.IDProdutoTipo, produtoTipoDTO.IDUsuario, produtoTipoDTO.NMProdutoTipo);
    }
}