using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoHistoricoService(IProdutoHistoricoRepository produtoHistoricoRepository) : IProdutoHistoricoService
    {
        private readonly IProdutoHistoricoRepository _produtoHistoricoRepository = produtoHistoricoRepository;

        public async Task<ProdutoHistoricoDTO?> RetornarProdutoHistoricoPorId(int idProdutoHistorico)
        {
            var produtoHistorico = await _produtoHistoricoRepository.RetornarProdutoHistoricoPorId(idProdutoHistorico);
            return produtoHistorico?.ParaDTO();
        }

        public async Task<ProdutoHistoricoDTO?> RetornarProdutoHistoricoPorIdEIdUsuario(int idProdutoHistorico, int idUsuario)
        {
            var produtoHistorico = await _produtoHistoricoRepository.RetornarProdutoHistoricoPorIdEIdUsuario(idProdutoHistorico, idUsuario);
            return produtoHistorico?.ParaDTO();
        }

        public async Task<List<ProdutoHistoricoDTO>> RetornarProdutosHistoricosPorIdUsuario(int idUsuario)
        {
            var produtoHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            return produtoHistoricos.ParaDTOs();
        }

        public async Task<List<ProdutoHistoricoDTO>> RetornarProdutosHistoricosPorIdProdutoEIdUsuario(int idProduto, int idUsuario)
        {
            var produtoHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdProdutoEIdUsuario(idProduto, idUsuario);
            return produtoHistoricos.ParaDTOs();
        }

        public async Task<Resultado<ProdutoHistoricoDTO>> CadastrarProdutoHistorico(ProdutoHistoricoDTO produtoHistorico)
        {
            var produtoHistoricoNova = await _produtoHistoricoRepository.CadastrarProdutoHistorico(produtoHistorico.ParaEntidade());
            if (produtoHistoricoNova == null) return Resultado<ProdutoHistoricoDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoHistoricoDTO>> AtualizarProdutoHistorico(ProdutoHistoricoDTO produtoHistorico)
        {
            try
            {
                var produtoHistoricoAtualizada = await _produtoHistoricoRepository.AtualizarProdutoHistorico(produtoHistorico.ParaEntidade());
                return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoHistoricoDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoHistorico(int idProdutoHistorico, int idUsuario)
        {
            var produtoHistoricoExcluido = await _produtoHistoricoRepository.ExcluirProdutoHistorico(idProdutoHistorico, idUsuario);
            if (produtoHistoricoExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoHistoricoDTO>> RetornarProdutoHistoricoAutentico(int idProdutoHistorico, int idUsuario)
        {
            var produtoHistoricoExistente = await RetornarProdutoHistoricoPorIdEIdUsuario(idProdutoHistorico, idUsuario);
            if (produtoHistoricoExistente == null) return Resultado<ProdutoHistoricoDTO>.Falha("Não encontrado");

            return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoExistente);
        }
    }
}