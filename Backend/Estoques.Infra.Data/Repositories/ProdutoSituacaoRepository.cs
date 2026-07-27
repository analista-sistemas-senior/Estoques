using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoSituacaoRepository(EstoquesDbContext context) : IProdutoSituacaoRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<ProdutoSituacao?> RetornarProdutoSituacaoPorId(int idProdutoSituacao)
        {
            return await _context.ProdutoSituacao.AsNoTracking().FirstOrDefaultAsync(ps => ps.IDProdutoSituacao == idProdutoSituacao);
        }

        public async Task<ProdutoSituacao?> RetornarProdutoSituacaoPorIdEIdUsuario(int idProdutoSituacao, int idUsuario)
        {
            return await _context.ProdutoSituacao.AsNoTracking().FirstOrDefaultAsync(ps => ps.IDProdutoSituacao == idProdutoSituacao && ps.IDUsuario == idUsuario);
        }

        public async Task<List<ProdutoSituacao>> RetornarProdutosSituacoesPorIdUsuario(int idUsuario)
        {
            return await _context.ProdutoSituacao.AsNoTracking().Where(ps => ps.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ProdutoSituacao> CadastrarProdutoSituacao(ProdutoSituacao produtoSituacao)
        {
            _context.ProdutoSituacao.Add(produtoSituacao);
            await _context.SaveChangesAsync();
            return produtoSituacao;
        }

        public async Task<ProdutoSituacao> AtualizarProdutoSituacao(ProdutoSituacao produtoSituacao)
        {
            _context.ProdutoSituacao.Update(produtoSituacao);
            await _context.SaveChangesAsync();
            return produtoSituacao;
        }

        public async Task<bool> ExcluirProdutoSituacao(int idProdutoSituacao, int idUsuario)
        {
            try { return await _context.ProdutoSituacao.Where(ps => ps.IDProdutoSituacao == idProdutoSituacao && ps.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}