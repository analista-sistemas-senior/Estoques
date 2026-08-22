namespace Estoques.Domain.Entities
{
    public class Usuario
    {
        public int IDUsuario { get; private set; }
        public string NMUsuario { get; private set; } = string.Empty;
        public string NMLogin { get; private set; } = string.Empty;
        public string CDSenha { get; private set; } = string.Empty;

        public virtual ICollection<Adquirente> Adquirentes { get; private set; } = [];
        public virtual ICollection<Fornecedor> Fornecedores { get; private set; } = [];
        public virtual ICollection<ProdutoTipo> ProdutosTipos { get; private set; } = [];
        public virtual ICollection<Produto> Produtos { get; private set; } = [];
        public virtual ICollection<ProdutoSituacao> ProdutosSituacoes { get; private set; } = [];
        public virtual ICollection<ProdutoFabricante> ProdutosFabricantes { get; private set; } = [];
        public virtual ICollection<ProdutoMedida> ProdutosMedidas { get; private set; } = [];

        public Usuario() { }
        public Usuario(int idUsuario, string nmUsuario, string nmLogin, string cdSenha)
        {
            IDUsuario = idUsuario;
            NMUsuario = nmUsuario;
            NMLogin = nmLogin;
            CDSenha = cdSenha;
        }
        public void DefinirSenhaCriptografada(string cdSenha)
        {
            CDSenha = cdSenha;
        }
    }
}