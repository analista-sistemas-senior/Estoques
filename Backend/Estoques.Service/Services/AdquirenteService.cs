using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class AdquirenteService(IAdquirenteRepository adquirenteRepository) : IAdquirenteService
    {
        private readonly IAdquirenteRepository _adquirenteRepository = adquirenteRepository;

        public async Task<AdquirenteDTO?> RetornarAdquirentePorId(int idAdquirente)
        {
            var adquirente = await _adquirenteRepository.RetornarAdquirentePorId(idAdquirente);
            return adquirente?.ParaDTO();
        }

        public async Task<AdquirenteDTO?> RetornarAdquirentePorIdEIdUsuario(int idAdquirente, int idUsuario)
        {
            var adquirente = await _adquirenteRepository.RetornarAdquirentePorIdEIdUsuario(idAdquirente, idUsuario);
            return adquirente?.ParaDTO();
        }

        public async Task<List<AdquirenteDTO>> RetornarAdquirentesPorIdUsuario(int idUsuario)
        {
            var adquirentes = await _adquirenteRepository.RetornarAdquirentesPorIdUsuario(idUsuario);
            return adquirentes.ParaDTOs();
        }

        public async Task<Resultado<AdquirenteDTO>> CadastrarAdquirente(AdquirenteDTO adquirente)
        {
            var adquirenteNova = await _adquirenteRepository.CadastrarAdquirente(adquirente.ParaEntidade());
            if (adquirenteNova == null) return Resultado<AdquirenteDTO>.Falha("Não cadastrado");

            return Resultado<AdquirenteDTO>.Ok(adquirenteNova.ParaDTO());
        }

        public async Task<Resultado<AdquirenteDTO>> AtualizarAdquirente(AdquirenteDTO adquirente)
        {
            try {
                var adquirenteAtualizada = await _adquirenteRepository.AtualizarAdquirente(adquirente.ParaEntidade());
                return Resultado<AdquirenteDTO>.Ok(adquirenteAtualizada.ParaDTO());
            }  catch (DbUpdateConcurrencyException) { return Resultado<AdquirenteDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirAdquirente(int idAdquirente, int idUsuario)
        {
            var adquirenteExcluido = await _adquirenteRepository.ExcluirAdquirente(idAdquirente, idUsuario);
            if (adquirenteExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<AdquirenteDTO>> RetornarAdquirenteAutentico(int idAdquirente, int idUsuario)
        {
            var adquirenteExistente = await RetornarAdquirentePorIdEIdUsuario(idAdquirente, idUsuario);
            if (adquirenteExistente == null) return Resultado<AdquirenteDTO>.Falha("Não encontrado");

            return Resultado<AdquirenteDTO>.Ok(adquirenteExistente);
        }
    }
}