using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bernal.ERP.ControleInventario.Dominio.Entidades
{
    public class Contagem
    {
        public int ContagemId { get; set; }
        public string Sessao { get; set; }
        public string Equipe { get; set; }
        public TipodeContagem Tipo { get; set; }
        
        public int InventarioItemId { get; set; }
       
        public InventarioItem InventarioItem { get; set; }


    }
}
