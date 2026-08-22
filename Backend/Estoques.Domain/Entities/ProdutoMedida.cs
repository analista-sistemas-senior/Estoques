namespace Estoques.Domain.Entities
{
    public class ProdutoMedida
    {
        public int IDProdutoMedida { get; private set; }
        public int IDUsuario { get; private set; }
        public string MDProdutoMedida { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Produto> Produtos { get; set; } = [];

        public ProdutoMedida() {}
        public ProdutoMedida(int idProdutoMedida, int idUsuario, string mdProdutoMedida)
        {
            IDProdutoMedida = idProdutoMedida;
            IDUsuario = idUsuario;
            MDProdutoMedida = mdProdutoMedida;
        }
    }
}