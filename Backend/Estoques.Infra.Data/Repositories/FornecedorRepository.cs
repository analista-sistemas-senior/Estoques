using Estoques.Domain.Entities;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Estoques.Infra.Data.Repositories
{
    public class FornecedorRepository(EstoquesDbContext context) : IFornecedorRepository
    {
        private readonly EstoquesDbContext _context = context;

        public async Task<Fornecedor?> RetornarFornecedorPorId(int idFornecedor)
        {
            return await _context.Fornecedor.AsNoTracking().FirstOrDefaultAsync(f => f.IDFornecedor == idFornecedor);
        }

        public async Task<Fornecedor?> RetornarFornecedorPorIdEIdUsuario(int idFornecedor, int idUsuario)
        {
            return await _context.Fornecedor.AsNoTracking().FirstOrDefaultAsync(f => f.IDFornecedor == idFornecedor && f.IDUsuario == idUsuario);
        }

        public async Task<List<Fornecedor>> RetornarFornecedoresPorIdUsuario(int idUsuario)
        {
            return await _context.Fornecedor.AsNoTracking().Where(f => f.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Fornecedor> CadastrarFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedor.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor> AtualizarFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedor.Update(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<bool> ExcluirFornecedor(int idFornecedor, int idUsuario)
        {
            try { return await _context.Fornecedor.Where(f => f.IDFornecedor == idFornecedor && f.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}