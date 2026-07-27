namespace Estoques.Domain.Entities
{
    public class ProdutoFabricante
    {
        public int IDProdutoFabricante { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMProdutoFabricante { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Produto> Produtos { get; set; } = [];

        public ProdutoFabricante() {}
        public ProdutoFabricante(int idProdutoFabricante, int idUsuario, string nmProdutoFabricante)
        {
            IDProdutoFabricante = idProdutoFabricante;
            IDUsuario = idUsuario;
            NMProdutoFabricante = nmProdutoFabricante;
        }
    }
}