using Estoques.Domain.Enums;

namespace Estoques.Domain.Entities
{
    public class Produto
    {
        public int IDProduto { get; private set; }
        public int IDProdutoTipo { get; private set; }
        public int IDProdutoSituacao { get; private set; }
        public int IDProdutoFabricante { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMProduto { get; private set; } = string.Empty;
        public string DSProduto { get; private set; } = string.Empty;
        public ProdutoCor INProdutoCor { get; private set; }
        public decimal QTProduto { get; private set; }
        public ProdutoMedida? INProdutoMedida { get; private set; }
        public string? LKProdutoImagem { get; private set; }

        public virtual ProdutoTipo ProdutoTipo { get; private set; } = null!;
        public virtual ProdutoSituacao ProdutoSituacao { get; private set; } = null!;
        public virtual ProdutoFabricante ProdutoFabricante { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<ProdutoHistorico> ProdutosHistoricos { get; set; } = [];

        public Produto() {}
        public Produto(int idProduto, int idProdutoTipo, int idProdutoSituacao, int idProdutoFabricante, int idUsuario, string nmProduto, string dsProduto, ProdutoCor inProdutoCor, decimal qtProduto, ProdutoMedida? inProdutoMedida, string? lkProdutoImagem)
        {
            IDProduto = idProduto;
            IDProdutoTipo = idProdutoTipo;
            IDProdutoSituacao = idProdutoSituacao;
            IDProdutoFabricante = idProdutoFabricante;
            IDUsuario = idUsuario;
            NMProduto = nmProduto;
            DSProduto = dsProduto;
            INProdutoCor = inProdutoCor;
            QTProduto = qtProduto;
            INProdutoMedida = inProdutoMedida;
            LKProdutoImagem = lkProdutoImagem;
        }
    }
}