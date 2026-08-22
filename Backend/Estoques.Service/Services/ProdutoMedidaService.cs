using Estoques.Domain.Interfaces;
using Estoques.Service.Common;
using Estoques.Service.DTOs;
using Estoques.Service.Interfaces;
using Estoques.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Service.Services
{
    public class ProdutoMedidaService(IProdutoMedidaRepository produtoMedidaRepository) : IProdutoMedidaService
    {
        private readonly IProdutoMedidaRepository _produtoMedidaRepository = produtoMedidaRepository;

        public async Task<ProdutoMedidaDTO?> RetornarProdutoMedidaPorId(int idProdutoMedida)
        {
            var produtoMedida = await _produtoMedidaRepository.RetornarProdutoMedidaPorId(idProdutoMedida);
            return produtoMedida?.ParaDTO();
        }

        public async Task<ProdutoMedidaDTO?> RetornarProdutoMedidaPorIdEIdUsuario(int idProdutoMedida, int idUsuario)
        {
            var produtoMedida = await _produtoMedidaRepository.RetornarProdutoMedidaPorIdEIdUsuario(idProdutoMedida, idUsuario);
            return produtoMedida?.ParaDTO();
        }

        public async Task<List<ProdutoMedidaDTO>> RetornarProdutosMedidasPorIdUsuario(int idUsuario)
        {
            var produtoMedidas = await _produtoMedidaRepository.RetornarProdutosMedidasPorIdUsuario(idUsuario);
            return produtoMedidas.ParaDTOs();
        }

        public async Task<Resultado<ProdutoMedidaDTO>> CadastrarProdutoMedida(ProdutoMedidaDTO produtoMedida)
        {
            var produtoMedidaNova = await _produtoMedidaRepository.CadastrarProdutoMedida(produtoMedida.ParaEntidade());
            if (produtoMedidaNova == null) return Resultado<ProdutoMedidaDTO>.Falha("Não cadastrado");

            return Resultado<ProdutoMedidaDTO>.Ok(produtoMedidaNova.ParaDTO());
        }

        public async Task<Resultado<ProdutoMedidaDTO>> AtualizarProdutoMedida(ProdutoMedidaDTO produtoMedida)
        {
            try
            {
                var produtoMedidaAtualizada = await _produtoMedidaRepository.AtualizarProdutoMedida(produtoMedida.ParaEntidade());
                return Resultado<ProdutoMedidaDTO>.Ok(produtoMedidaAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ProdutoMedidaDTO>.Falha("Não atualizado"); }
        }

        public async Task<Resultado<bool>> ExcluirProdutoMedida(int idProdutoMedida, int idUsuario)
        {
            var produtoMedidaExcluido = await _produtoMedidaRepository.ExcluirProdutoMedida(idProdutoMedida, idUsuario);
            if (produtoMedidaExcluido) return Resultado<bool>.Ok(true);
            else return Resultado<bool>.Falha("Não excluído");
        }

        public async Task<Resultado<ProdutoMedidaDTO>> RetornarProdutoMedidaAutentico(int idProdutoMedida, int idUsuario)
        {
            var produtoMedidaExistente = await RetornarProdutoMedidaPorIdEIdUsuario(idProdutoMedida, idUsuario);
            if (produtoMedidaExistente == null) return Resultado<ProdutoMedidaDTO>.Falha("Não encontrado");

            return Resultado<ProdutoMedidaDTO>.Ok(produtoMedidaExistente);
        }
    }
}