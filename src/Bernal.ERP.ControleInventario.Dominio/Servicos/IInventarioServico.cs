using Bernal.ERP.ControleInventario.Dominio.Entidades;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
namespace Bernal.ERP.ControleInventario.Dominio.Servicos
{
    public interface IInventarioServico
    {
        Task<Inventario> SaveAsync();

        ValidationResult InserirInventario(Inventario inventario);
        Task<ValidationResult> AtualizarInventarioAsync(Inventario inventario);
        Task<Inventario> DeleteInventarioAsync(int inventarioId);
        Task<Inventario> ReverterInventarioAsync(int inventarioId);
        Task<List<Inventario>> ObterInventarioTodosAsync();
        Task<Inventario> ObterInventarioPorIdAsync(int inventarioId);
        Task<List<Inventario>> ObterInventarioSomenteExcluidosAsync();

        ValidationResult InserirInventarioItem(InventarioItem inventarioItem);
        Task<ValidationResult> AtualizarInventarioItemAsync(InventarioItem inventarioItem);
        Task<InventarioItem> DeleteInventarioItemAsync(int inventarioId, int inventarioItemId);
        Task<InventarioItem> ReverterInventarioItemAsync(int inventarioId, int inventarioItemId);
        Task<List<InventarioItem>> ObterInventarioItemTodosAsync(int inventarioId);
        Task<InventarioItem> ObterInventarioItemPorIdAsync(int inventarioId, int inventarioItemId);
        Task<InventarioItem> ObterInventarioItemPorNomeAsync(int inventarioId, string nome);
        Task<List<InventarioItem>> ObterInventarioItemSomenteExcluidosAsync();

        ValidationResult InserirInventarioDispositivo(InventarioDispositivo inventarioDispositivo);
        Task<ValidationResult> AtualizarInventarioDispositivoAsync(InventarioDispositivo inventarioDispositivo);
        Task<InventarioDispositivo> DeleteInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId);
        Task<InventarioDispositivo> ReverterInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId);
        Task<List<InventarioDispositivo>> ObterInventarioDispositivoTodosAsync(int inventarioId);
        Task<InventarioDispositivo> ObterInventarioDispositivoPorIdAsync(int inventarioId, int inventarioDispositivoId);
        Task<InventarioDispositivo> ObterInventarioDispositivoPorNumeroSerieAsync(int inventarioId, string numeroDeSerie);
        Task<List<InventarioDispositivo>> ObterInventarioDispositivosSomenteExcluidosAsync();

    }
    }

