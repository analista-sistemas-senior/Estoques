using Estoques.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Estoques.Service.DTOs.Produto
{
    public record ProdutoSaidaDTO(int IDProduto, int IDProdutoTipo, int IDProdutoSituacao, int IDProdutoFabricante, int IDUsuario, int? IDProdutoMedida, string NMProduto, string? DSProduto, ProdutoCor INProdutoCor, decimal? QTProduto, string? LKProdutoImagem, string? TXAnotacao, ProdutoTipoDTO? ProdutoTipo, ProdutoSituacaoDTO? ProdutoSituacao, ProdutoFabricanteDTO? ProdutoFabricante, ProdutoMedidaDTO? ProdutoMedida, IFormFile? Arquivo);
}