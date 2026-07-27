using Estoques.Domain.Enums;

namespace Estoques.Service.DTOs
{
    public record ProdutoHistoricoDTO(int IDProdutoHistorico, int IDProduto, int IDFornecedor, int? IDAdquirente, ProdutoHistoricoTipo INProdutoHistoricoTipo, DateTime DTProdutoHistorico, decimal QTProdutoHistorico, decimal VLProdutoHistorico, ProdutoDTO? Produto, FornecedorDTO? Fornecedor, AdquirenteDTO? Adquirente);
}