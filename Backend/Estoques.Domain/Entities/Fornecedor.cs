namespace Estoques.Domain.Entities
{
    public class Fornecedor
    {
        public int IDFornecedor { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMFornecedor { get; private set; } = string.Empty;
        public string? TXEndereco { get; private set; }
        public string? TXAnotacao { get; private set; }

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<ProdutoHistorico> ProdutosHistoricos { get; set; } = [];

        public Fornecedor() {}
        public Fornecedor(int idFornecedor, int idUsuario, string nmFornecedor, string? txEndereco, string? txAnotacao)
        {
            IDFornecedor = idFornecedor;
            IDUsuario = idUsuario;
            NMFornecedor = nmFornecedor;
            TXEndereco = txEndereco;
            TXAnotacao = txAnotacao;
        }
    }
}