using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class FornecedorService(IFornecedorRepository fornecedorRepository) : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository = fornecedorRepository;

        public async Task<FornecedorDTO?> RetornarFornecedorPorId(int idFornecedor)
        {
            var fornecedor = await _fornecedorRepository.RetornarFornecedorPorId(idFornecedor);
            return fornecedor?.ParaDTO();
        }

        public async Task<FornecedorDTO?> RetornarFornecedorPorIdEIdUsuario(int idFornecedor, int idUsuario)
        {
            var fornecedor = await _fornecedorRepository.RetornarFornecedorPorIdEIdUsuario(idFornecedor, idUsuario);
            return fornecedor?.ParaDTO();
        }

        public async Task<List<FornecedorDTO>> RetornarFornecedoresPorIdUsuario(int idUsuario)
        {
            var fornecedors = await _fornecedorRepository.RetornarFornecedoresPorIdUsuario(idUsuario);
            return fornecedors.ParaDTOs();
        }

        public async Task<Resultado<FornecedorDTO>> CadastrarFornecedor(FornecedorDTO fornecedor)
        {
            var fornecedorNova = await _fornecedorRepository.CadastrarFornecedor(fornecedor.ParaEntidade());
            if (fornecedorNova == null) return Resultado<FornecedorDTO>.Falha("Não cadastrado");

            return Resultado<FornecedorDTO>.Ok(fornecedorNova.ParaDTO());
        }

        public async Task<Resultado<FornecedorDTO>> AtualizarFornecedor(FornecedorDTO fornecedor)
        {
            try
            {
                var fornecedorAtualizada = await _fornecedorRepository.AtualizarFornecedor(fornecedor.ParaEntidade());
                return Resultado<FornecedorDTO>.Ok(fornecedorAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<FornecedorDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirFornecedor(int idFornecedor, int idUsuario)
        {
            var fornecedorExcluido = await _fornecedorRepository.ExcluirFornecedor(idFornecedor, idUsuario);
            if (fornecedorExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<FornecedorDTO>> RetornarFornecedorAutentico(int idFornecedor, int idUsuario)
        {
            var fornecedorExistente = await RetornarFornecedorPorIdEIdUsuario(idFornecedor, idUsuario);
            if (fornecedorExistente == null) return Resultado<FornecedorDTO>.Falha("Não encontrado");

            return Resultado<FornecedorDTO>.Ok(fornecedorExistente);
        }
    }
}