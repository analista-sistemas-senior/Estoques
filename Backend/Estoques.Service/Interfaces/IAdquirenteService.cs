using Estoques.Service.Common;
using Estoques.Service.DTOs;

namespace Estoques.Service.Interfaces
{
    public interface IAdquirenteService
    {
        Task<AdquirenteDTO?> RetornarAdquirentePorId(int idAdquirente);
        Task<AdquirenteDTO?> RetornarAdquirentePorIdEIdUsuario(int idAdquirente, int idUsuario);
        Task<List<AdquirenteDTO>> RetornarAdquirentesPorIdUsuario(int idUsuario);
        Task<Resultado<AdquirenteDTO>> CadastrarAdquirente(AdquirenteDTO adquirente);
        Task<Resultado<AdquirenteDTO>> AtualizarAdquirente(AdquirenteDTO adquirente);
        Task<Resultado<bool>> ExcluirAdquirente(int idAdquirente, int idUsuario);
        Task<Resultado<AdquirenteDTO>> RetornarAdquirenteAutentico(int idAdquirente, int idUsuario);
    }
}