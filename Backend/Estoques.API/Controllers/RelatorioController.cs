using Estoques.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Estoques.Service.DTOs.Relatorio;
using Estoques.Service.Interfaces;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class RelatorioController(IRelatorioService relatorioService) : ControladorBase
    {
        private readonly IRelatorioService _relatorioService = relatorioService;

        [HttpGet("relatorios/totais")]
        public async Task<ActionResult<IEnumerable<RelatorioTotalDTO>>> RetornarTotais()
        {
            var relatorio = await _relatorioService.RetornarRelatorioTotal(IDUsuarioLogado);
            return relatorio == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(relatorio);
        }

        [HttpGet("relatorios/produtos/tipos")]
        public async Task<ActionResult<IEnumerable<RelatorioProdutoTipoDTO>>> RetornarProdutoPorTipo()
        {
            var relatorio = await _relatorioService.RetornarRelatorioProdutoPorTipo(IDUsuarioLogado);
            return relatorio == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(relatorio);
        }

        [HttpGet("relatorios/produtos/fabricantes")]
        public async Task<ActionResult<IEnumerable<RelatorioProdutoFabricanteDTO>>> RetornarProdutoPorFabricante()
        {
            var relatorio = await _relatorioService.RetornarRelatorioProdutoPorFabricante(IDUsuarioLogado);
            return relatorio == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(relatorio);
        }

        [HttpGet("relatorios/produtos/fornecedores")]
        public async Task<ActionResult<IEnumerable<RelatorioProdutoFornecedorDTO>>> RetornarProdutoPorFornecedor()
        {
            var relatorio = await _relatorioService.RetornarRelatorioProdutoPorFornecedor(IDUsuarioLogado);
            return relatorio == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(relatorio);
        }

        [HttpGet("relatorios/produtos/cores")]
        public async Task<ActionResult<IEnumerable<RelatorioProdutoFornecedorDTO>>> RetornarProdutoPorCor()
        {
            var relatorio = await _relatorioService.RetornarRelatorioProdutoPorCor(IDUsuarioLogado);
            return relatorio == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(relatorio);
        }
    }
}