using Bernal.ERP.ControleInventario.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bernal.ERP.ControleInventario.Dominio.Repositorios
{
    public interface IInventarioRepositorio
    {
        Task<Inventario> SaveAsync();

        void AdicionarInventario(Inventario inventario);
        Task<Inventario> AtualizarInventarioAsync(Inventario inventario);
        Task<Inventario> DeleteInventarioAsync(int inventarioId);
        Task<Inventario> ReverterInventarioAsync(int inventarioId);
        Task<List<Inventario>> ObterInventarioTodosAsync(int skip = 0, int take = 200);
        Task<Inventario> ObterInventarioPorIdAsync(int inventarioId);
        Task<List<Inventario>> ObterInventarioSomenteExcluidosAsync();

        void AdicionarInventarioItem(InventarioItem inventarioItem);
        Task<InventarioItem> AtualizarInventarioItemAsync(InventarioItem inventarioItem);
        Task<InventarioItem> DeleteInventarioItemAsync(int inventarioId, int inventarioItemId);
        Task<InventarioItem> ReverterInventarioItemAsync(int inventarioId, int inventarioItemId);
        Task<List<InventarioItem>> ObterInventarioItemTodosAsync(int inventarioId, int skip = 0, int take = 200);
        Task<InventarioItem> ObterInventarioItemPorIdAsync(int inventarioId, int inventarioItemId);
        Task<InventarioItem> ObterInventarioItemPorNomeAsync(int inventarioId, string nome);
        Task<List<InventarioItem>> ObterInventarioItemSomenteExcluidosAsync();

        void AdicionarInventarioDispositivo(InventarioDispositivo inventarioDispositivo);
        Task<InventarioDispositivo> AtualizarInventarioDispositivoAsync(InventarioDispositivo inventarioDispositivo);
        Task<InventarioDispositivo> DeleteInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId);
        Task<InventarioDispositivo> ReverterInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId);
        Task<List<InventarioDispositivo>> ObterInventarioDispositivoTodosAsync(int inventarioId, int skip = 0, int take = 200);
        Task<InventarioDispositivo> ObterInventarioDispositivoPorIdAsync(int inventarioId, int inventarioDispositivoId);
        Task<InventarioDispositivo> ObterInventarioDispositivoPorNumeroSerieAsync(int inventarioId, string numeroDeSerie);
        Task<List<InventarioDispositivo>> ObterInventarioDispositivosSomenteExcluidosAsync();

    }
}
