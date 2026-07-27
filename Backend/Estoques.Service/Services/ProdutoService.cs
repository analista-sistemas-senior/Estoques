using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<ProdutoDTO?> RetornarProdutoPorId(int idProduto)
        {
            var produto = await _produtoRepository.RetornarProdutoPorId(idProduto);
            return produto?.ParaDTO();
        }

        public async Task<ProdutoDTO?> RetornarProdutoPorIdEIdUsuario(int idProduto, int idUsuario)
        {
            var produto = await _produtoRepository.RetornarProdutoPorIdEIdUsuario(idProduto, idUsuario);
            return produto?.ParaDTO();
        }

        public async Task<List<ProdutoDTO>> RetornarProdutosPorIdUsuario(int idUsuario)
        {
            var produtos = await _produtoRepository.RetornarProdutosPorIdUsuario(idUsuario);
            return produtos.ParaDTOs();
        }

        public async Task<Resultado<ProdutoDTO>> CadastrarProduto(ProdutoDTO produto)
        {
            var produtoNova = await _produtoRepository.CadastrarProduto(produto.ParaEntidade());
            if (produtoNova == null) return Resultado<ProdutoDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoDTO>.Ok(produtoNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoDTO>> AtualizarProduto(ProdutoDTO produto)
        {
            try
            {
                var produtoAtualizada = await _produtoRepository.AtualizarProduto(produto.ParaEntidade());
                return Resultado<ProdutoDTO>.Ok(produtoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProduto(int idProduto, int idUsuario)
        {
            var produtoExcluido = await _produtoRepository.ExcluirProduto(idProduto, idUsuario);
            if (produtoExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoDTO>> RetornarProdutoAutentico(int idProduto, int idUsuario)
        {
            var produtoExistente = await RetornarProdutoPorIdEIdUsuario(idProduto, idUsuario);
            if (produtoExistente == null) return Resultado<ProdutoDTO>.Falha("Não encontrado");

            return Resultado<ProdutoDTO>.Ok(produtoExistente);
        }
    }
}