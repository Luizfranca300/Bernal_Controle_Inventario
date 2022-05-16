using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Bernal.ERP.ControleInventario.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bernal.ERP.ControleInventario.Infra.Repositorios
{
    public class InventarioRepositorio : IInventarioRepositorio
    {

        private readonly DbSet<Inventario> _inventarios;
        private readonly DataContext _context;
        private readonly DbSet<InventarioItem> _inventarioItems;
        private readonly DbSet<InventarioDispositivo> _inventarioDispositivos;


        public InventarioRepositorio(DataContext dataContext)
        {
            _context = dataContext;
            _inventarios = dataContext.Set<Inventario>();
            _inventarioItems = dataContext.Set<InventarioItem>();
            _inventarioDispositivos = dataContext.Set<InventarioDispositivo>();
        }
        public async Task<Inventario> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return null;
        }

        private IIncludableQueryable<Inventario, ICollection<InventarioItem>> IncluirTodos()
        {
            return _inventarios
                            .Include(c => c.InventarioDispositivos)
                            .Include(c => c.InventarioItems).ThenInclude(c => c.Contagem)
                            .Include(c => c.InventarioItems);
        }


        public void AdicionarInventario(Inventario inventario)
        {
            _inventarios.Add(inventario);

        }
        public async Task<Inventario> AtualizarInventarioAsync(Inventario inventario)
        {
            var itemdoBanco = await _inventarios
                .FirstOrDefaultAsync(c => c.InventarioId == inventario.InventarioId);

            if (itemdoBanco != null)
            {
                _context.Entry(itemdoBanco).CurrentValues.SetValues(inventario);

                return inventario;
            }
            return null;
        }
        public async Task<Inventario> DeleteInventarioAsync(int inventarioId)
        {
            var result = await _inventarios                
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = DateTime.Now;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<Inventario> ReverterInventarioAsync(int inventarioId)
        {
            var result = await _inventarios
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = null;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<List<Inventario>> ObterInventarioTodosAsync(int skip = 0, int take = 200)
        {
            var result = await _inventarios
                          .Include(c => c.InventarioDispositivos)
                          .AsNoTracking()
                          .Skip(skip)
                          .Take(take)
                          .ToListAsync();
            return result;
        }
        public async Task<Inventario> ObterInventarioPorIdAsync(int inventarioId)
        {

            var result = await IncluirTodos()
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            return result;

        }
        public async Task<List<Inventario>> ObterInventarioSomenteExcluidosAsync()
        {
            var result = await IncluirTodos()
                .IgnoreQueryFilters()
                .Where(c => c.ExcluidoEm.HasValue)
                .ToListAsync();

            return result;
        }

        
        public void AdicionarInventarioItem(InventarioItem inventarioItem)
        {
            _inventarioItems.Add(inventarioItem);

        }
        public async Task<InventarioItem> AtualizarInventarioItemAsync(InventarioItem inventarioItem)
        {
            var itemdoBanco = await _inventarioItems
                .Where(c =>c.InventarioItemId == inventarioItem.InventarioItemId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioItem.InventarioId);

            if (itemdoBanco != null)
            {
                _context.Entry(itemdoBanco).CurrentValues.SetValues(inventarioItem);

                return inventarioItem;
            }
            return null;
        }
        public async Task<InventarioItem> DeleteInventarioItemAsync(int inventarioId, int inventarioItemId)
        {
            var result = await _inventarioItems
                .Where(c => c.InventarioItemId == inventarioItemId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = DateTime.Now;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<InventarioItem> ReverterInventarioItemAsync(int inventarioId, int inventarioItemId)
        {
            var result = await _inventarioItems
                .IgnoreQueryFilters()
                .Where(c => c.InventarioItemId == inventarioItemId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = null;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<List<InventarioItem>> ObterInventarioItemTodosAsync(int inventarioId,int skip = 0, int take = 200)
        {
            var result = await _inventarioItems
                          .Where(c =>c.InventarioId == inventarioId)
                          .Skip(skip)
                          .Take(take)
                          .ToListAsync();
            return result;
        }
        public async Task<InventarioItem> ObterInventarioItemPorIdAsync(int inventarioId, int inventarioItemId)
        {
            var result = await _inventarioItems
                .Include(c => c.Contagem)
                .Where(c=>c.InventarioItemId == inventarioItemId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);
            return result;
        }
        public async Task<InventarioItem> ObterInventarioItemPorNomeAsync(int inventarioId, string nome)
        {
            var result = await _inventarioItems
                .Where(c => c.ProdutoNome == nome)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);
            return result;
        }
        public async Task<List<InventarioItem>> ObterInventarioItemSomenteExcluidosAsync()
        {
            var result = await _inventarioItems
                .IgnoreQueryFilters()
                .Where(c => c.ExcluidoEm.HasValue)
                .ToListAsync();

            return result;
        }


        public void AdicionarInventarioDispositivo(InventarioDispositivo inventarioDispositivo)
        {
            _inventarioDispositivos.Add(inventarioDispositivo);

        }
        public async Task<InventarioDispositivo> AtualizarInventarioDispositivoAsync(InventarioDispositivo inventarioDispositivo)
        {
            var itemdoBanco = await _inventarioDispositivos
                .Where(c => c.InventarioDispositivoId == inventarioDispositivo.InventarioDispositivoId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioDispositivo.InventarioId);

            if (itemdoBanco != null)
            {
                _context.Entry(itemdoBanco).CurrentValues.SetValues(inventarioDispositivo);

                return inventarioDispositivo;
            }
            return null;
        }
        public async Task<InventarioDispositivo> DeleteInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId)
        {
            var result = await _inventarioDispositivos
                .Where(c => c.InventarioDispositivoId == inventarioDispositivoId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = DateTime.Now;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<InventarioDispositivo> ReverterInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId)
        {
            var result = await _inventarioDispositivos
                .IgnoreQueryFilters()
                .Where(c => c.InventarioDispositivoId == inventarioDispositivoId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

            if (result != null)
            {
                result.ExcluidoEm = null;
                _context.Entry(result).State = EntityState.Modified;
                return result;
            }
            return null;
        }
        public async Task<List<InventarioDispositivo>> ObterInventarioDispositivoTodosAsync(int inventarioId, int skip = 0, int take = 200)
        {
            var result = await _inventarioDispositivos
                          .Where(c => c.InventarioId == inventarioId)
                          .Skip(skip)
                          .Take(take)
                          .ToListAsync();
            return result;
        }
        public async Task<InventarioDispositivo> ObterInventarioDispositivoPorIdAsync(int inventarioId, int inventarioDispositivoId)
        {
            var result = await _inventarioDispositivos
                .Where(c => c.InventarioDispositivoId == inventarioDispositivoId)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);
            return result;
        }
        public async Task<InventarioDispositivo> ObterInventarioDispositivoPorNumeroSerieAsync(int inventarioId, string numeroDeSerie)
        {
            var result = await _inventarioDispositivos
                .Where(c => c.NumeroDeSerie == numeroDeSerie)
                .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);
            return result;
        }
        public async Task<List<InventarioDispositivo>> ObterInventarioDispositivosSomenteExcluidosAsync()
        {
            var result = await _inventarioDispositivos
                .IgnoreQueryFilters()
                .Where(c => c.ExcluidoEm.HasValue)
                .ToListAsync();

            return result;
        }




















        /*

                public async Task<ICollection<InventarioItem>> ObterPorInventariosNomeAsync(int inventarioId, string nome)
                {
                    var inventarioComItemFiltrado = await _inventarios
                        .Include(c => c.InventarioItems.Where(c => c.ProdutoNome == nome))
                        .FirstOrDefaultAsync(c => c.InventarioId == inventarioId);

                    return inventarioComItemFiltrado?.InventarioItems;
                }

                public async Task<List<InventarioItem>> ObterEmTodosInvantariosPorNomeAsync(string nome)
                {
                    var itens = await _inventarios
                        .AsNoTracking()
                        .SelectMany(c => c.InventarioItems)
                        .Where(c => c.ProdutoNome == nome)
                        .ToListAsync();


                    return itens;
                }

        */










    }
}
