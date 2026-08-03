using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Service.DTOs.Relatorio;
using Estoques.Service.Interfaces;

namespace Estoques.Service.Services
{
    public class RelatorioService(IProdutoRepository produtoRepository, IProdutoHistoricoRepository produtoHistoricoRepository) : IRelatorioService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IProdutoHistoricoRepository _produtoHistoricoRepository = produtoHistoricoRepository;

        public async Task<RelatorioTotalDTO> RetornarRelatorioTotal(int idUsuario)
        {
            decimal qtTotalProduto = 0;
            decimal vlTotalProduto = 0;
            decimal qtTotalComprado = 0;
            decimal vlTotalComprado = 0;
            decimal qtTotalVendido = 0;
            decimal vlTotalVendido = 0;

            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            if (produtosHistoricos != null)
            {
                foreach (var historico in produtosHistoricos)
                {
                    if (historico.INProdutoHistoricoTipo == Domain.Enums.ProdutoHistoricoTipo.Compra)
                    {
                        qtTotalComprado += historico.QTProdutoHistorico;
                        vlTotalComprado += historico.QTProdutoHistorico * historico.VLProdutoHistorico;

                        qtTotalProduto += historico.QTProdutoHistorico;
                        vlTotalProduto += historico.QTProdutoHistorico * historico.VLProdutoHistorico;
                    }
                    else
                    {
                        qtTotalVendido += historico.QTProdutoHistorico;
                        vlTotalVendido += historico.QTProdutoHistorico * historico.VLProdutoHistorico;

                        qtTotalProduto -= historico.QTProdutoHistorico;
                        vlTotalProduto -= historico.QTProdutoHistorico * historico.VLProdutoHistorico;
                    }
                }
            }

            return new RelatorioTotalDTO(qtTotalProduto, vlTotalProduto, qtTotalComprado, vlTotalComprado, qtTotalVendido, vlTotalVendido);
        }

        public async Task<List<RelatorioProdutoTipoDTO>> RetornarRelatorioProdutoPorTipo(int idUsuario)
        {
            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            if (produtosHistoricos == null) return [];

            return [.. produtosHistoricos.GroupBy(p => p.Produto.ProdutoTipo.NMProdutoTipo).Select(r => new RelatorioProdutoTipoDTO(r.Key, r.Sum(ph => ph.QTProdutoHistorico))) ];
        }

        public async Task<List<RelatorioProdutoFabricanteDTO>> RetornarRelatorioProdutoPorFabricante(int idUsuario)
        {
            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            if (produtosHistoricos == null) return [];

            return [.. produtosHistoricos.GroupBy(p => p.Produto.ProdutoFabricante.NMProdutoFabricante).Select(r => new RelatorioProdutoFabricanteDTO(r.Key, r.Sum(ph => ph.QTProdutoHistorico))) ];
        }

        public async Task<List<RelatorioProdutoFornecedorDTO>> RetornarRelatorioProdutoPorFornecedor(int idUsuario)
        {
            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            if (produtosHistoricos == null) return [];

            return [.. produtosHistoricos.GroupBy(ph => ph.Fornecedor.NMFornecedor).Select(r => new RelatorioProdutoFornecedorDTO(r.Key, r.Sum(ph => ph.QTProdutoHistorico))) ];
        }

        public async Task<List<RelatorioProdutoCorDTO>> RetornarRelatorioProdutoPorCor(int idUsuario)
        {
            var produtosHistoricos = await _produtoHistoricoRepository.RetornarProdutosHistoricosPorIdUsuario(idUsuario);
            if (produtosHistoricos == null) return [];

            return [.. produtosHistoricos.GroupBy(p => p.Produto.INProdutoCor).Select(r => new RelatorioProdutoCorDTO(r.Key.ToString(), r.Sum(ph => ph.QTProdutoHistorico))) ];
        }
    }
}