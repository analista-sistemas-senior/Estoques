using Estoques.Domain.Enums;

namespace Estoques.Domain.Entities
{
    public class ProdutoHistorico
    {
        public int IDProdutoHistorico { get; private set; }
        public int IDProduto { get; private set; }
        public int IDFornecedor { get; private set; }
        public int? IDAdquirente { get; private set; }
        public ProdutoHistoricoTipo INProdutoHistoricoTipo { get; private set; }
        public DateTime DTProdutoHistorico { get; private set; }
        public decimal QTProdutoHistorico { get; private set; }
        public decimal VLProdutoHistorico { get; private set; }

        public virtual Produto Produto { get; private set; } = null!;
        public virtual Fornecedor Fornecedor { get; private set; } = null!;
        public virtual Adquirente? Adquirente { get; private set; }

        public ProdutoHistorico() {}
        public ProdutoHistorico(int idProdutoHistorico, int idProduto, int idFornecedor, int? idAdquirente, ProdutoHistoricoTipo inProdutoHistoricoTipo, DateTime dtProdutoHistorico, decimal qtProdutoHistorico, decimal vlProdutoHistorico)
        {
            IDProdutoHistorico = idProdutoHistorico;
            IDProduto = idProduto;
            IDFornecedor = idFornecedor;
            IDAdquirente = idAdquirente;
            INProdutoHistoricoTipo = inProdutoHistoricoTipo;
            DTProdutoHistorico = dtProdutoHistorico;
            QTProdutoHistorico = qtProdutoHistorico;
            VLProdutoHistorico = vlProdutoHistorico;
        }
    }
}