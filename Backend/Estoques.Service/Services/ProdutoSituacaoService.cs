using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoSituacaoService(IProdutoSituacaoRepository produtoSituacaoRepository) : IProdutoSituacaoService
    {
        private readonly IProdutoSituacaoRepository _produtoSituacaoRepository = produtoSituacaoRepository;

        public async Task<ProdutoSituacaoDTO?> RetornarProdutoSituacaoPorId(int idProdutoSituacao)
        {
            var produtoSituacao = await _produtoSituacaoRepository.RetornarProdutoSituacaoPorId(idProdutoSituacao);
            return produtoSituacao?.ParaDTO();
        }

        public async Task<ProdutoSituacaoDTO?> RetornarProdutoSituacaoPorIdEIdUsuario(int idProdutoSituacao, int idUsuario)
        {
            var produtoSituacao = await _produtoSituacaoRepository.RetornarProdutoSituacaoPorIdEIdUsuario(idProdutoSituacao, idUsuario);
            return produtoSituacao?.ParaDTO();
        }

        public async Task<List<ProdutoSituacaoDTO>> RetornarProdutoSituacaosPorIdUsuario(int idUsuario)
        {
            var produtoSituacaos = await _produtoSituacaoRepository.RetornarProdutosSituacoesPorIdUsuario(idUsuario);
            return produtoSituacaos.ParaDTOs();
        }

        public async Task<Resultado<ProdutoSituacaoDTO>> CadastrarProdutoSituacao(ProdutoSituacaoDTO produtoSituacao)
        {
            var produtoSituacaoNova = await _produtoSituacaoRepository.CadastrarProdutoSituacao(produtoSituacao.ParaEntidade());
            if (produtoSituacaoNova == null) return Resultado<ProdutoSituacaoDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoSituacaoDTO>.Ok(produtoSituacaoNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoSituacaoDTO>> AtualizarProdutoSituacao(ProdutoSituacaoDTO produtoSituacao)
        {
            try
            {
                var produtoSituacaoAtualizada = await _produtoSituacaoRepository.AtualizarProdutoSituacao(produtoSituacao.ParaEntidade());
                return Resultado<ProdutoSituacaoDTO>.Ok(produtoSituacaoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoSituacaoDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoSituacao(int idProdutoSituacao, int idUsuario)
        {
            var produtoSituacaoExcluido = await _produtoSituacaoRepository.ExcluirProdutoSituacao(idProdutoSituacao, idUsuario);
            if (produtoSituacaoExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoSituacaoDTO>> RetornarProdutoSituacaoAutentico(int idProdutoSituacao, int idUsuario)
        {
            var produtoSituacaoExistente = await RetornarProdutoSituacaoPorIdEIdUsuario(idProdutoSituacao, idUsuario);
            if (produtoSituacaoExistente == null) return Resultado<ProdutoSituacaoDTO>.Falha("Não encontrado");

            return Resultado<ProdutoSituacaoDTO>.Ok(produtoSituacaoExistente);
        }
    }
}