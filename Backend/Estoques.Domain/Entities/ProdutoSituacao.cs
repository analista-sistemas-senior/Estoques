namespace Estoques.Domain.Entities
{
    public class ProdutoSituacao
    {
        public int IDProdutoSituacao { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMProdutoSituacao { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Produto> Produtos { get; set; } = [];

        public ProdutoSituacao() {}
        public ProdutoSituacao(int idProdutoSituacao, int idUsuario, string nmProdutoSituacao)
        {
            IDProdutoSituacao = idProdutoSituacao;
            IDUsuario = idUsuario;
            NMProdutoSituacao = nmProdutoSituacao;
        }
    }
}