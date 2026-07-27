using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoFabricanteService(IProdutoFabricanteRepository produtoFabricanteRepository) : IProdutoFabricanteService
    {
        private readonly IProdutoFabricanteRepository _produtoFabricanteRepository = produtoFabricanteRepository;

        public async Task<ProdutoFabricanteDTO?> RetornarProdutoFabricantePorId(int idProdutoFabricante)
        {
            var produtoFabricante = await _produtoFabricanteRepository.RetornarProdutoFabricantePorId(idProdutoFabricante);
            return produtoFabricante?.ParaDTO();
        }

        public async Task<ProdutoFabricanteDTO?> RetornarProdutoFabricantePorIdEIdUsuario(int idProdutoFabricante, int idUsuario)
        {
            var produtoFabricante = await _produtoFabricanteRepository.RetornarProdutoFabricantePorIdEIdUsuario(idProdutoFabricante, idUsuario);
            return produtoFabricante?.ParaDTO();
        }

        public async Task<List<ProdutoFabricanteDTO>> RetornarProdutosFabricantesPorIdUsuario(int idUsuario)
        {
            var produtoFabricantes = await _produtoFabricanteRepository.RetornarProdutosFabricantesPorIdUsuario(idUsuario);
            return produtoFabricantes.ParaDTOs();
        }

        public async Task<Resultado<ProdutoFabricanteDTO>> CadastrarProdutoFabricante(ProdutoFabricanteDTO produtoFabricante)
        {
            var produtoFabricanteNova = await _produtoFabricanteRepository.CadastrarProdutoFabricante(produtoFabricante.ParaEntidade());
            if (produtoFabricanteNova == null) return Resultado<ProdutoFabricanteDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoFabricanteDTO>.Ok(produtoFabricanteNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoFabricanteDTO>> AtualizarProdutoFabricante(ProdutoFabricanteDTO produtoFabricante)
        {
            try
            {
                var produtoFabricanteAtualizada = await _produtoFabricanteRepository.AtualizarProdutoFabricante(produtoFabricante.ParaEntidade());
                return Resultado<ProdutoFabricanteDTO>.Ok(produtoFabricanteAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoFabricanteDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoFabricante(int idProdutoFabricante, int idUsuario)
        {
            var produtoFabricanteExcluido = await _produtoFabricanteRepository.ExcluirProdutoFabricante(idProdutoFabricante, idUsuario);
            if (produtoFabricanteExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoFabricanteDTO>> RetornarProdutoFabricanteAutentico(int idProdutoFabricante, int idUsuario)
        {
            var produtoFabricanteExistente = await RetornarProdutoFabricantePorIdEIdUsuario(idProdutoFabricante, idUsuario);
            if (produtoFabricanteExistente == null) return Resultado<ProdutoFabricanteDTO>.Falha("Não encontrado");

            return Resultado<ProdutoFabricanteDTO>.Ok(produtoFabricanteExistente);
        }
    }
}