using Estoques.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Estoques.Service.DTOs
{
    public record ProdutoDTO(int IDProduto, int IDProdutoTipo, int IDProdutoSituacao, int IDProdutoFabricante, int IDUsuario, string NMProduto, string DSProduto, ProdutoCor INProdutoCor, decimal QTProduto, ProdutoMedida? INProdutoMedida, string? LKProdutoImagem, ProdutoTipoDTO? ProdutoTipo, ProdutoSituacaoDTO? ProdutoSituacao, ProdutoFabricanteDTO? ProdutoFabricante, IFormFile? Arquivo);
}