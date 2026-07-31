using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoHistoricoService(IProdutoHistoricoRepository produtoHistoricoRepository, IProdutoRepository produtoRepository) : IProdutoHistoricoService
    {
        private readonly IProdutoHistoricoRepository _produtoHistoricoRepository = produtoHistoricoRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

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

            await AtualizaProdutoQuantidade(produtoHistoricoNova);

            return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoHistoricoDTO>> AtualizarProdutoHistorico(ProdutoHistoricoDTO produtoHistorico)
        {
            try
            {
                var produtoHistoricoAtualizada = await _produtoHistoricoRepository.AtualizarProdutoHistorico(produtoHistorico.ParaEntidade());

                await AtualizaProdutoQuantidade(produtoHistoricoAtualizada);

                return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoHistoricoDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoHistorico(int idProdutoHistorico, int idUsuario)
        {
            var produtoHistorico = await _produtoHistoricoRepository.RetornarProdutoHistoricoPorId(idProdutoHistorico);

            var produtoHistoricoExcluido = await _produtoHistoricoRepository.ExcluirProdutoHistorico(idProdutoHistorico, idUsuario);
            if (produtoHistoricoExcluido)
            {
                if (produtoHistorico != null) await AtualizaProdutoQuantidade(produtoHistorico);

                return Resultado<bool>.Ok(true);
            } else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoHistoricoDTO>> RetornarProdutoHistoricoAutentico(int idProdutoHistorico, int idUsuario)
        {
            var produtoHistoricoExistente = await RetornarProdutoHistoricoPorIdEIdUsuario(idProdutoHistorico, idUsuario);
            if (produtoHistoricoExistente == null) return Resultado<ProdutoHistoricoDTO>.Falha("Não encontrado");

            return Resultado<ProdutoHistoricoDTO>.Ok(produtoHistoricoExistente);
        }

        private async Task AtualizaProdutoQuantidade(ProdutoHistorico produtoHistorico)
        {
            var produto = await _produtoRepository.RetornarProdutoPorId(produtoHistorico.IDProduto);
            if (produto == null) return;

            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdProdutoEIdUsuario(produtoHistorico.IDProduto, produto.IDUsuario);
            if (produtosHistoricos == null) return;

            var total = produtosHistoricos.Where(ph => ph.INProdutoHistoricoTipo == Domain.Enums.ProdutoHistoricoTipo.Compra).Sum(ph => ph.QTProdutoHistorico);
            total += -1 * produtosHistoricos.Where(ph => ph.INProdutoHistoricoTipo == Domain.Enums.ProdutoHistoricoTipo.Venda).Sum(ph => ph.QTProdutoHistorico);

            if (produto == null) return;

            produto.AtribuirQuantidade(total);

            await _produtoRepository.AtualizarProduto(produto);
        }
    }
}