using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Bernal.ERP.ControleInventario.Dominio.Repositorios;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Bernal.ERP.ControleInventario.Dominio.Servicos
{
    public class InventarioServico : IInventarioServico 
    {
        private readonly IInventarioRepositorio _inventarioRepositorio;

        public InventarioServico(IInventarioRepositorio inventarioRepositorio)
        {
            _inventarioRepositorio = inventarioRepositorio;
        }


        public async Task<ValidationResult> AtualizarInventarioAsync(Inventario inventario)
        {
            if (!inventario.EhValido())
            {
                return inventario.ValidacaoResultado;
            }

            await _inventarioRepositorio.AtualizarInventarioAsync(inventario);
            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarInventarioDispositivoAsync(InventarioDispositivo inventarioDispositivo)
        {
            if (!inventarioDispositivo.EhValido())
            {
                return inventarioDispositivo.ValidacaoResultado;
            }

            await _inventarioRepositorio.AtualizarInventarioDispositivoAsync(inventarioDispositivo);
            return new ValidationResult();
        }

        public async Task<ValidationResult> AtualizarInventarioItemAsync(InventarioItem inventarioItem)
        {
            if (!inventarioItem.EhValido())
            {
                return inventarioItem.ValidacaoResultado;
            }

            await _inventarioRepositorio.AtualizarInventarioItemAsync(inventarioItem);
            return new ValidationResult();
        }

        public async Task<Inventario> DeleteInventarioAsync(int inventarioId)
        {
            return await _inventarioRepositorio.DeleteInventarioAsync(inventarioId);
        }

        public async Task<InventarioDispositivo> DeleteInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId)
        {
            return await _inventarioRepositorio.DeleteInventarioDispositivoAsync(inventarioId,inventarioDispositivoId);
        }

        public async Task<InventarioItem> DeleteInventarioItemAsync(int inventarioId, int inventarioItemId)
        {
            return await _inventarioRepositorio.DeleteInventarioItemAsync(inventarioId,inventarioItemId);
        }

        public ValidationResult InserirInventario(Inventario inventario)
        {
            if (!inventario.EhValido())
            {
                return inventario.ValidacaoResultado;
            }

            _inventarioRepositorio.AdicionarInventario(inventario);
            return new ValidationResult();
        }

        public ValidationResult InserirInventarioDispositivo(InventarioDispositivo inventarioDispositivo)
        {
            if (!inventarioDispositivo.EhValido())
            {
                return inventarioDispositivo.ValidacaoResultado;
            }

            _inventarioRepositorio.AdicionarInventarioDispositivo(inventarioDispositivo);
            return new ValidationResult();
        }

        public ValidationResult InserirInventarioItem(InventarioItem inventarioItem)
        {
            if (!inventarioItem.EhValido())
            {
                return inventarioItem.ValidacaoResultado;
            }

            _inventarioRepositorio.AdicionarInventarioItem(inventarioItem);
            return new ValidationResult();
        }

        public async Task<InventarioDispositivo> ObterInventarioDispositivoPorIdAsync(int inventarioId, int inventarioDispositivoId)
        {
            return await _inventarioRepositorio.ObterInventarioDispositivoPorIdAsync(inventarioId, inventarioDispositivoId);
        }

        public async Task<InventarioDispositivo> ObterInventarioDispositivoPorNumeroSerieAsync(int inventarioId, string numeroDeSerie)
        {
            return await _inventarioRepositorio.ObterInventarioDispositivoPorNumeroSerieAsync(inventarioId, numeroDeSerie);
        }

        public async Task<List<InventarioDispositivo>> ObterInventarioDispositivosSomenteExcluidosAsync()
        {
            return await _inventarioRepositorio.ObterInventarioDispositivosSomenteExcluidosAsync();
        }

        public async Task<List<InventarioDispositivo>> ObterInventarioDispositivoTodosAsync(int inventarioId)
        {
            return await _inventarioRepositorio.ObterInventarioDispositivoTodosAsync(inventarioId);
        }

        public async Task<InventarioItem> ObterInventarioItemPorIdAsync(int inventarioId, int inventarioItemId)
        {
            return await _inventarioRepositorio.ObterInventarioItemPorIdAsync(inventarioId, inventarioItemId);
        }

        public async Task<InventarioItem> ObterInventarioItemPorNomeAsync(int inventarioId, string nome)
        {
            return await _inventarioRepositorio.ObterInventarioItemPorNomeAsync(inventarioId, nome);
        }

        public async Task<List<InventarioItem>> ObterInventarioItemSomenteExcluidosAsync()
        {
            return await _inventarioRepositorio.ObterInventarioItemSomenteExcluidosAsync();
        }

        public async Task<List<InventarioItem>> ObterInventarioItemTodosAsync(int inventarioId)
        {
            return await _inventarioRepositorio.ObterInventarioItemTodosAsync(inventarioId);
        }

        public async Task<Inventario> ObterInventarioPorIdAsync(int inventarioId)
        {
            return await _inventarioRepositorio.ObterInventarioPorIdAsync(inventarioId);
        }

        public async Task<List<Inventario>> ObterInventarioSomenteExcluidosAsync()
        {
            return await _inventarioRepositorio.ObterInventarioSomenteExcluidosAsync();
        }

        public async Task<List<Inventario>> ObterInventarioTodosAsync()
        {
            return await _inventarioRepositorio.ObterInventarioTodosAsync();
        }

        public async Task<Inventario> ReverterInventarioAsync(int inventarioId)
        {
            return await _inventarioRepositorio.ReverterInventarioAsync(inventarioId);
        }

        public async Task<InventarioDispositivo> ReverterInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId)
        {
            return await _inventarioRepositorio.ReverterInventarioDispositivoAsync(inventarioId, inventarioDispositivoId);
        }

        public async Task<InventarioItem> ReverterInventarioItemAsync(int inventarioId, int inventarioItemId)
        {
            return await _inventarioRepositorio.ReverterInventarioItemAsync(inventarioId, inventarioItemId);
        }

        public async Task<Inventario> SaveAsync()
        {
            return await _inventarioRepositorio.SaveAsync();
        }



    }
    
       
   
}

