using Estoques.API.Controllers.Base;
using Estoques.API.Services;
using Estoques.Service.DTOs.Produto;
using Estoques.Service.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estoques.API.Controllers
{
    [Authorize]
    public class ProdutoController(IProdutoService produtoService, IValidator<ProdutoEntradaDTO> validador, IWebHostEnvironment ambiente, IArmazenamentoService armazenamentoService) : ControladorBase
    {
        private readonly IProdutoService _produtoService = produtoService;
        private readonly IValidator<ProdutoEntradaDTO> _validador = validador;
        private readonly IWebHostEnvironment _ambiente = ambiente;
        private readonly IArmazenamentoService _armazenamentoService = armazenamentoService;

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<ProdutoSaidaDTO>>> RetornarProdutosPorIdUsuario()
        {
            var produtos = await _produtoService.RetornarProdutosPorIdUsuario(IDUsuarioLogado);
            return produtos == null ? NotFound(new { mensagem = "Não encontrado" }) : Ok(produtos);
        }

        [HttpPost("produtos")]
        public async Task<IActionResult> CadastrarProduto([FromForm] ProdutoEntradaDTO produtoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            string? LinkProdutoImagem = null;
            if (produtoDTO.Arquivo != null && produtoDTO.Arquivo.Length > 0) LinkProdutoImagem = await _armazenamentoService.SalvarArquivo(produtoDTO.Arquivo);

            var produto = produtoDTO with {
                IDUsuario = IDUsuarioLogado,
                LKProdutoImagem = LinkProdutoImagem
            };

            try 
            { 
                var produtoResultado = await _produtoService.CadastrarProduto(produto);
                if (!produtoResultado.Sucesso)
                {
                    return BadRequest(new { mensagem = produtoResultado.MensagemErro });
                }
                else
                {
                    return CreatedAtAction(nameof(CadastrarProduto), new { id = produtoResultado.Dados!.IDProduto }, produtoResultado.Dados);
                }
                
            }
            catch (DbUpdateException) { return BadRequest(new { mensagem = "Não cadastrado" }); }
        }

        [HttpPut("produtos")]
        public async Task<IActionResult> AtualizarProduto([FromForm] ProdutoEntradaDTO produtoDTO)
        {
            var validacaoDTO = await _validador.ValidateAsync(produtoDTO);
            if (!validacaoDTO.IsValid) return BadRequest(new { mensagem = validacaoDTO.Errors.FirstOrDefault()?.ErrorMessage });

            string? LinkProdutoImagem = produtoDTO.LKProdutoImagem;
            if (produtoDTO.Arquivo != null && produtoDTO.Arquivo.Length > 0)
            {
                _armazenamentoService.ExcluirArquivo(produtoDTO.LKProdutoImagem);
                LinkProdutoImagem = await _armazenamentoService.SalvarArquivo(produtoDTO.Arquivo);
            }

            var produto = produtoDTO with { 
                IDUsuario = IDUsuarioLogado, 
                LKProdutoImagem = LinkProdutoImagem
            };

            try
            {
                var produtoResultado = await _produtoService.AtualizarProduto(produto);
                if (!produtoResultado.Sucesso) return NotFound(new { mensagem = produtoResultado.MensagemErro });
                return NoContent();
            }
            catch (DbUpdateException) { return BadRequest(new { mensagem = "Não atualizado" }); }
        }

        [HttpDelete("produtos/{id:int}")]
        public async Task<IActionResult> ExcluirProduto(int id)
        {
            var produto = await _produtoService.RetornarProdutoPorId(id);
            if (produto == null) return NotFound(new { mensagem = "Não excluído" });

            var produtoResultado = await _produtoService.ExcluirProduto(id, IDUsuarioLogado);
            if (!produtoResultado.Sucesso)
            {
                return NotFound(new { mensagem = produtoResultado.MensagemErro });
            }
            else
            {
                if (produto.LKProdutoImagem != "")
                {
                    if (!string.IsNullOrEmpty(produto.LKProdutoImagem)) _armazenamentoService.ExcluirArquivo(produto.LKProdutoImagem);
                }
            }

            return NoContent();
        }
    }
}