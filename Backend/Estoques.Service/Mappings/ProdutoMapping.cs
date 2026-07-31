using Estoques.Domain.Entities;
using Estoques.Service.DTOs.Produto;

namespace Estoques.Service.Mappings;

public static class ProdutoMapping
{
    public static ProdutoSaidaDTO ParaDTO(this Produto produto)
    {
        return new ProdutoSaidaDTO(produto.IDProduto, produto.IDProdutoTipo, produto.IDProdutoSituacao, produto.IDProdutoFabricante, produto.IDUsuario, produto.NMProduto, produto.DSProduto, produto.INProdutoCor, produto.QTProduto, produto.INProdutoMedida, produto.LKProdutoImagem, produto.ProdutoTipo?.ParaDTO(), produto.ProdutoSituacao?.ParaDTO(), produto.ProdutoFabricante?.ParaDTO(), null);
    }

    public static List<ProdutoSaidaDTO> ParaDTOs(this List<Produto> produtos)
    {
        return [.. produtos.Select(p => p.ParaDTO()).ToList()];
    }

    public static Produto ParaEntidade(this ProdutoEntradaDTO produtoDTO)
    {
        return new Produto(produtoDTO.IDProduto, produtoDTO.IDProdutoTipo, produtoDTO.IDProdutoSituacao, produtoDTO.IDProdutoFabricante, produtoDTO.IDUsuario, produtoDTO.NMProduto, produtoDTO.DSProduto, produtoDTO.INProdutoCor, null, produtoDTO.INProdutoMedida, produtoDTO.LKProdutoImagem);
    }
}