namespace Estoques.Domain.Entities
{
    public class Adquirente
    {
        public int IDAdquirente { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMAdquirente { get; private set; } = string.Empty;
        public string? TXEndereco { get; private set; }
        public string? TXAnotacao { get; private set; }

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<ProdutoHistorico>? ProdutosHistoricos { get; set; }

        public Adquirente() {}
        public Adquirente(int idAdquirente, int idUsuario, string nmAdquirente, string? txEndereco, string? txAnotacao)
        {
            IDAdquirente = idAdquirente;
            IDUsuario = idUsuario;
            NMAdquirente = nmAdquirente;
            TXEndereco = txEndereco;
            TXAnotacao = txAnotacao;
        }
    }
}