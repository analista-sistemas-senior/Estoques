using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class ProdutoHistoricoRepository(EstoquesDbContext context) : IProdutoHistoricoRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<ProdutoHistorico?> RetornarProdutoHistoricoPorId(int idProdutoHistorico)
        {
            return await _context.ProdutoHistorico.AsNoTracking().Include(ph => ph.Produto).Include(ph => ph.Fornecedor).Include(ph => ph.Adquirente).FirstOrDefaultAsync(ph => ph.IDProdutoHistorico == idProdutoHistorico);
        }

        public async Task<ProdutoHistorico?> RetornarProdutoHistoricoPorIdEIdUsuario(int idProdutoHistorico, int idUsuario)
        {
            return await _context.ProdutoHistorico.AsNoTracking().Include(ph => ph.Produto).Include(ph => ph.Fornecedor).Include(ph => ph.Adquirente).FirstOrDefaultAsync(ph => ph.IDProdutoHistorico == idProdutoHistorico && ph.Produto.IDUsuario == idUsuario);
        }

        public async Task<List<ProdutoHistorico>> RetornarProdutosHistoricosPorIdUsuario(int idUsuario)
        {
            return await _context.ProdutoHistorico.AsNoTracking().Include(ph => ph.Produto).Include(ph => ph.Fornecedor).Include(ph => ph.Adquirente).Where(ph => ph.Produto.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<List<ProdutoHistorico>> RetornarProdutosHistoricosPorIdProdutoEIdUsuario(int idProduto, int idUsuario)
        {
            return await _context.ProdutoHistorico.AsNoTracking().Include(ph => ph.Produto).Include(ph => ph.Fornecedor).Include(ph => ph.Adquirente).Where(ph => ph.IDProduto == idProduto && ph.Produto.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ProdutoHistorico> CadastrarProdutoHistorico(ProdutoHistorico produtoHistorico)
        {
            _context.ProdutoHistorico.Add(produtoHistorico);
            await _context.SaveChangesAsync();
            return produtoHistorico;
        }

        public async Task<ProdutoHistorico> AtualizarProdutoHistorico(ProdutoHistorico produtoHistorico)
        {
            _context.ProdutoHistorico.Update(produtoHistorico);
            await _context.SaveChangesAsync();
            return produtoHistorico;
        }

        public async Task<bool> ExcluirProdutoHistorico(int idProdutoHistorico, int idUsuario)
        {
            try { return await _context.ProdutoHistorico.Where(ph => ph.IDProdutoHistorico == idProdutoHistorico && ph.Produto.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}