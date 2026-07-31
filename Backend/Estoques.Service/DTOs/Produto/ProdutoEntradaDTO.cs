using Estoques.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Estoques.Service.DTOs.Produto
{
    public record ProdutoEntradaDTO(int IDProduto, int IDProdutoTipo, int IDProdutoSituacao, int IDProdutoFabricante, int IDUsuario, string NMProduto, string? DSProduto, ProdutoCor INProdutoCor, ProdutoMedida? INProdutoMedida, string? LKProdutoImagem, ProdutoTipoDTO? ProdutoTipo, ProdutoSituacaoDTO? ProdutoSituacao, ProdutoFabricanteDTO? ProdutoFabricante, IFormFile? Arquivo);
}