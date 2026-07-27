using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class ProdutoSituacaoMapping
{
    public static ProdutoSituacaoDTO ParaDTO(this ProdutoSituacao produtoSituacao)
    {
        return new ProdutoSituacaoDTO(produtoSituacao.IDProdutoSituacao, produtoSituacao.IDUsuario, produtoSituacao.NMProdutoSituacao);
    }

    public static List<ProdutoSituacaoDTO> ParaDTOs(this List<ProdutoSituacao> produtoSituacaos)
    {
        return [.. produtoSituacaos.Select(ps => ps.ParaDTO()).ToList()];
    }

    public static ProdutoSituacao ParaEntidade(this ProdutoSituacaoDTO produtoSituacaoDTO)
    {
        return new ProdutoSituacao(produtoSituacaoDTO.IDProdutoSituacao, produtoSituacaoDTO.IDUsuario, produtoSituacaoDTO.NMProdutoSituacao);
    }
}