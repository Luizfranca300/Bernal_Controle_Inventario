using Bernal.ERP.ControleInventario.Dominio.Entidades;
using Bernal.ERP.ControleInventario.Dominio.Servicos;
using Bernal.ERP.ControleInventario.Infra;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bernal.ERP.ControleInventario.Api.Controllers
{
    [ApiController]
    [Route(template: "V1/inventarios")]
    public class ControleInventarioController  : ControllerBase
    {
    
        private readonly IInventarioServico _inventarioServico;

        public ControleInventarioController(DataContext context, 
            IInventarioServico inventarioServico)
        {

            _inventarioServico = inventarioServico;
        }

        //Post /v1/inventarios
        [HttpPost]
        public async Task<IActionResult> InserirInventarioAsync([FromBody] Inventario inventario)
        {
            var result = _inventarioServico.InserirInventario(inventario);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventario);
            }
            return BadRequest(result.Errors);

        }
        //Put /V1/inventarios/{inventarioId}
        [HttpPut("{inventarioId}")]
        public async Task<IActionResult> AtualizarInventarioAsync(int inventarioId, Inventario inventario)
        {
            if( inventario.InventarioId != inventarioId)
            {
                return BadRequest("O inventarioId está invalido");
            }

            var result = await _inventarioServico.AtualizarInventarioAsync(inventario);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventario);

            }
            return BadRequest();
        }
        //Delete /v1/inventarios/{inventarioId}
        [HttpDelete("{inventarioId}")]
        public async Task<IActionResult> DeleteInventarioAsync(int inventarioId)
        {
            var result = await _inventarioServico.DeleteInventarioAsync(inventarioId);
            if (result != null)
            {
                await _inventarioServico.SaveAsync();
                return Ok(result);

            }
            return NotFound();
        }
        //Get /v1/inventarios -> Todos /v1/inventarios?somenteexcluidos=true -> mostra os inventarios excluidos
        [HttpGet]
        public async Task<IActionResult> ObterInventarioAsync(bool somenteexcluidos)
        {
            if (somenteexcluidos)
            {
                var excluidos = await _inventarioServico.ObterInventarioSomenteExcluidosAsync();
                return Ok(excluidos);
            }

            var todos = await _inventarioServico.ObterInventarioTodosAsync();
            return Ok(todos);
        }
        //Get /v1/inventarios/{inventarioId}
        [HttpGet("{inventarioId}")]
        public async Task<IActionResult> ObterInventarioPorIdAsync(int inventarioId)
        {
            var result = await _inventarioServico.ObterInventarioPorIdAsync(inventarioId);
            return result == null
                  ? NotFound()
                  : Ok(result);
        }
        //post /v1/inventarios/{inventarioId}/?reverterinventario=true
        [HttpPost("{inventarioId}")]
        public async Task<IActionResult> RerverterInventarioAsync(bool reverterinventario, int inventarioId)
        {
            if (reverterinventario)
            {
                var result = await _inventarioServico.ReverterInventarioAsync(inventarioId);
                await _inventarioServico.SaveAsync();
                return Ok(result);
            }
       
            return BadRequest();

        }


        //Post /v1/inventarios/itens
        [HttpPost("{inventarioId}/itens")]
        public async Task<IActionResult> InserirInventarioItemAsync([FromBody] InventarioItem inventarioItem, int inventarioId)
        {
            if (inventarioItem.InventarioId != inventarioId)
            {
                return BadRequest("O inventarioId está invalido");
            }

            var result = _inventarioServico.InserirInventarioItem(inventarioItem);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventarioItem);
            }
            return BadRequest(result.Errors);

        }
        
        //Put /V1/inventarios/{inventarioId}/itens/{inventarioItemId}
        [HttpPut("{inventarioId}/itens/{inventarioItemId}")]
        public async Task<IActionResult> AtualizarInventarioItemAsync(int inventarioId, int inventarioItemId, InventarioItem inventarioItem)
        {
            if (inventarioItem.InventarioId != inventarioId && inventarioItem.InventarioItemId == inventarioItemId)
            {
                return BadRequest("O inventarioId ou InventarioDispositivoId está invalido");
            }

            var result = await _inventarioServico.AtualizarInventarioItemAsync(inventarioItem);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventarioItem);

            }
            return BadRequest();
        }

        //Delete /v1/inventarios/{inventarioId}/itens/{inventarioItemId}
        [HttpDelete("{inventarioId}/itens/{inventarioItemId}")]
        public async Task<IActionResult> DeleteInventarioItemAsync(int inventarioId, int inventarioItemId)
        {
            var result = await _inventarioServico.DeleteInventarioItemAsync(inventarioId, inventarioItemId);
            if (result != null)
            {
                await _inventarioServico.SaveAsync();
                return Ok(result);

            }
            return NotFound();
        }

        //Get /V1/inventarios/{inventarioId}/itens -> Todos
        [HttpGet("{inventarioId}/itens")]
        public async Task<IActionResult> ObterInventarioItemAsync(int inventarioId, bool somenteexcluidos, string nome)
        {
            if (somenteexcluidos)
            {
                var excluidos = await _inventarioServico.ObterInventarioItemSomenteExcluidosAsync();
                return Ok(excluidos);
            }

            if(nome != null)
            {
                var inventariopornome = await _inventarioServico.ObterInventarioItemPorNomeAsync(inventarioId,nome);
                return Ok(inventariopornome);
            }

            var todos = await _inventarioServico.ObterInventarioItemTodosAsync(inventarioId);
            return Ok(todos);
        }

        //Get /v1/inventarios/{inventarioId}/itens/{inventarioItemId}
        [HttpGet("{inventarioId}/itens/{inventarioItemId}")]
        public async Task<IActionResult> ObterInventarioItemPorIdAsync(int inventarioId, int inventarioItemId)
        {
            var result = await _inventarioServico.ObterInventarioItemPorIdAsync(inventarioId, inventarioItemId);
            return result == null
                  ? NotFound()
                  : Ok(result);
        }
        //post /v1/inventarios/{inventarioId}/itens/{inventarioItemId}?reverterinventario=true
        [HttpPost("{inventarioId}/itens/{inventarioItemId}")]
        public async Task<IActionResult> RerverterInventarioItemAsync( int inventarioId, int inventarioItemId, bool reverterinventario)
        {
            if (reverterinventario)
            {
                var result = await _inventarioServico.ReverterInventarioItemAsync(inventarioId, inventarioItemId);
                await _inventarioServico.SaveAsync();
                return Ok(result);
            }

            return BadRequest();

        }


        //Post /v1/inventarios/dispositivos
        [HttpPost("{inventarioId}/dispositivos")]
        public async Task<IActionResult> InserirInventarioDispositivoAsync([FromBody] InventarioDispositivo inventarioDispositivo, int inventarioId)
        {
            if (inventarioDispositivo.InventarioId != inventarioId)
            {
                return BadRequest("O inventarioId está invalido");
            }

            var result = _inventarioServico.InserirInventarioDispositivo(inventarioDispositivo);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventarioDispositivo);
            }
            return BadRequest(result.Errors);

        }

        //Put /V1/inventarios/{inventarioId}/dispositivos/{inventarioItemId}
        [HttpPut("{inventarioId}/dispositivos/{inventarioItemId}")]
        public async Task<IActionResult> AtualizarInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId, InventarioDispositivo inventarioDispositivo)
        {
            if (inventarioDispositivo.InventarioId != inventarioId && inventarioDispositivo.InventarioDispositivoId == inventarioDispositivoId)
            {
                return BadRequest("O inventarioId ou InventarioDispositivoId está invalido");
            }

            var result = await _inventarioServico.AtualizarInventarioDispositivoAsync(inventarioDispositivo);
            if (result.IsValid)
            {
                await _inventarioServico.SaveAsync();
                return Ok(inventarioDispositivo);

            }
            return BadRequest();
        }

        //Delete /v1/inventarios/{inventarioId}/dispositivos/{inventarioDispositivoId}
        [HttpDelete("{inventarioId}/dispositivos/{inventarioDispositivoId}")]
        public async Task<IActionResult> DeleteInventarioDispositivoAsync(int inventarioId, int inventarioDispositivoId)
        {
            var result = await _inventarioServico.DeleteInventarioDispositivoAsync(inventarioId, inventarioDispositivoId);
            if (result != null)
            {
                await _inventarioServico.SaveAsync();
                return Ok(result);

            }
            return NotFound();
        }

        //Get /V1/inventarios/{inventarioId}/dispositivos -> Todos
        [HttpGet("{inventarioId}/dispositivos")]
        public async Task<IActionResult> ObterInventarioDispositivosAsync(int inventarioId, bool somenteexcluidos, string numeroDeSerie)
        {
            if (somenteexcluidos)
            {
                var excluidos = await _inventarioServico.ObterInventarioDispositivosSomenteExcluidosAsync();
                return Ok(excluidos);
            }

            if (numeroDeSerie != null)
            {
                var inventariopornome = await _inventarioServico.ObterInventarioDispositivoPorNumeroSerieAsync(inventarioId, numeroDeSerie);
                return Ok(inventariopornome);
            }

            var todos = await _inventarioServico.ObterInventarioDispositivoTodosAsync(inventarioId);
            return Ok(todos);
        }

        //Get /v1/inventarios/{inventarioId}/dispositivos/{inventarioItemId}
        [HttpGet("{inventarioId}/dispositivos/{inventarioDispositivoId}")]
        public async Task<IActionResult> ObterInventarioDispositivosPorIdAsync(int inventarioId, int inventarioDispositivoId)
        {
            var result = await _inventarioServico.ObterInventarioDispositivoPorIdAsync(inventarioId, inventarioDispositivoId);
            return result == null
                  ? NotFound()
                  : Ok(result);
        }
        //post /v1/inventarios/{inventarioId}/dispositivos/{inventarioDispositivoId}?reverterinventario=true
        [HttpPost("{inventarioId}/dispositivos/{inventarioDispositivoId}")]
        public async Task<IActionResult> RerverterInventarioDispositivoAsync(bool reverterinventario, int inventarioId, int inventarioDispositivoId)
        {
            if (reverterinventario)
            {
                var result = await _inventarioServico.ReverterInventarioDispositivoAsync(inventarioId, inventarioDispositivoId);
                await _inventarioServico.SaveAsync();
                return Ok(result);
            }

            return BadRequest();

        }





    }
}
 