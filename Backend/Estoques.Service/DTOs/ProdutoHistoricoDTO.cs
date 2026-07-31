using Estoques.Domain.Enums;
using Estoques.Service.DTOs.Produto;

namespace Estoques.Service.DTOs
{
    public record ProdutoHistoricoDTO(int IDProdutoHistorico, int IDProduto, int IDFornecedor, int? IDAdquirente, ProdutoHistoricoTipo INProdutoHistoricoTipo, DateTime DTProdutoHistorico, decimal QTProdutoHistorico, decimal VLProdutoHistorico, ProdutoSaidaDTO? Produto, FornecedorDTO? Fornecedor, AdquirenteDTO? Adquirente);
}