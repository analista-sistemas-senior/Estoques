namespace Estoques.Domain.Entities
{
    public class ProdutoTipo
    {
        public int IDProdutoTipo { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMProdutoTipo { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Produto> Produtos { get; set; } = [];

        public ProdutoTipo() { }
        public ProdutoTipo(int idProdutoTipo, int idUsuario, string nmProdutoTipo)
        {
            IDProdutoTipo = idProdutoTipo;
            IDUsuario = idUsuario;
            NMProdutoTipo = nmProdutoTipo;
        }
    }
}