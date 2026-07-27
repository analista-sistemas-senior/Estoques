namespace Estoques.Service.Common
{
    public class Resultado<T>
    {
        public bool Sucesso { get; private set; }
        public T? Dados { get; private set; }
        public string MensagemErro { get; private set; } = string.Empty;

        public static Resultado<T> Ok(T dados)
        {
            return new Resultado<T> { Sucesso = true, Dados = dados };
        }
        public static Resultado<T> Falha(string erro)
        {
            return new Resultado<T> { Sucesso = false, MensagemErro = erro };
        }
    }
}