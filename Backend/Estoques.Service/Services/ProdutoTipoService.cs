using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoTipoService(IProdutoTipoRepository produtoTipoRepository) : IProdutoTipoService
    {
        private readonly IProdutoTipoRepository _produtoTipoRepository = produtoTipoRepository;

        public async Task<ProdutoTipoDTO?> RetornarProdutoTipoPorId(int idProdutoTipo)
        {
            var produtoTipo = await _produtoTipoRepository.RetornarProdutoTipoPorId(idProdutoTipo);
            return produtoTipo?.ParaDTO();
        }

        public async Task<ProdutoTipoDTO?> RetornarProdutoTipoPorIdEIdUsuario(int idProdutoTipo, int idUsuario)
        {
            var produtoTipo = await _produtoTipoRepository.RetornarProdutoTipoPorIdEIdUsuario(idProdutoTipo, idUsuario);
            return produtoTipo?.ParaDTO();
        }

        public async Task<List<ProdutoTipoDTO>> RetornarProdutoTiposPorIdUsuario(int idUsuario)
        {
            var produtoTipos = await _produtoTipoRepository.RetornarProdutosTiposPorIdUsuario(idUsuario);
            return produtoTipos.ParaDTOs();
        }

        public async Task<Resultado<ProdutoTipoDTO>> CadastrarProdutoTipo(ProdutoTipoDTO produtoTipo)
        {
            var produtoTipoNova = await _produtoTipoRepository.CadastrarProdutoTipo(produtoTipo.ParaEntidade());
            if (produtoTipoNova == null) return Resultado<ProdutoTipoDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoTipoDTO>.Ok(produtoTipoNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoTipoDTO>> AtualizarProdutoTipo(ProdutoTipoDTO produtoTipo)
        {
            try
            {
                var produtoTipoAtualizada = await _produtoTipoRepository.AtualizarProdutoTipo(produtoTipo.ParaEntidade());
                return Resultado<ProdutoTipoDTO>.Ok(produtoTipoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoTipoDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoTipo(int idProdutoTipo, int idUsuario)
        {
            var produtoTipoExcluido = await _produtoTipoRepository.ExcluirProdutoTipo(idProdutoTipo, idUsuario);
            if (produtoTipoExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoTipoDTO>> RetornarProdutoTipoAutentico(int idProdutoTipo, int idUsuario)
        {
            var produtoTipoExistente = await RetornarProdutoTipoPorIdEIdUsuario(idProdutoTipo, idUsuario);
            if (produtoTipoExistente == null) return Resultado<ProdutoTipoDTO>.Falha("Não encontrado");

            return Resultado<ProdutoTipoDTO>.Ok(produtoTipoExistente);
        }
    }
}