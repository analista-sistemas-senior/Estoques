using Estoques.Domain.Entities;
using Estoques.Service.DTOs;

namespace Estoques.Service.Mappings;

public static class ProdutoHistoricoMapping
{
    public static ProdutoHistoricoDTO ParaDTO(this ProdutoHistorico produtoHistorico)
    {
        return new ProdutoHistoricoDTO(produtoHistorico.IDProdutoHistorico, produtoHistorico.IDProduto, produtoHistorico.IDFornecedor, produtoHistorico.IDAdquirente, produtoHistorico.INProdutoHistoricoTipo, produtoHistorico.DTProdutoHistorico, produtoHistorico.QTProdutoHistorico, produtoHistorico.VLProdutoHistorico, produtoHistorico.Produto?.ParaDTO(), produtoHistorico.Fornecedor?.ParaDTO(), produtoHistorico.Adquirente?.ParaDTO());
    }

    public static List<ProdutoHistoricoDTO> ParaDTOs(this List<ProdutoHistorico> produtoHistoricos)
    {
        return [.. produtoHistoricos.Select(ph => ph.ParaDTO()).ToList()];
    }

    public static ProdutoHistorico ParaEntidade(this ProdutoHistoricoDTO produtoHistoricoDTO)
    {
        return new ProdutoHistorico(produtoHistoricoDTO.IDProdutoHistorico, produtoHistoricoDTO.IDProduto, produtoHistoricoDTO.IDFornecedor, produtoHistoricoDTO.IDAdquirente, produtoHistoricoDTO.INProdutoHistoricoTipo, produtoHistoricoDTO.DTProdutoHistorico, produtoHistoricoDTO.QTProdutoHistorico, produtoHistoricoDTO.VLProdutoHistorico);
    }
}