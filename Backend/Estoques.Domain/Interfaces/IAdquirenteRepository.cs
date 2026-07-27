using Estoques.Domain.Entities;

namespace Estoques.Domain.Interfaces
{
    public interface IAdquirenteRepository
    {
        Task<Adquirente?> RetornarAdquirentePorId(int idAdquirente);
        Task<Adquirente?> RetornarAdquirentePorIdEIdUsuario(int idAdquirente, int idUsuario);
        Task<List<Adquirente>> RetornarAdquirentesPorIdUsuario(int idUsuario);
        Task<Adquirente> CadastrarAdquirente(Adquirente adquirente);
        Task<Adquirente> AtualizarAdquirente(Adquirente adquirente);
        Task<bool> ExcluirAdquirente(int idAdquirente, int idUsuario);
    }
}