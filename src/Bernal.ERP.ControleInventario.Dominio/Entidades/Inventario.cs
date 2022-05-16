using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bernal.ERP.ControleInventario.Dominio.Entidades
{
    public class Inventario: Entidade
    {
    

        public int InventarioId { get; set; }
        public DateTime IniciadoEm { get; set; } = DateTime.Now;
        public DateTime? FinalizadoEm { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public ICollection<InventarioDispositivo> InventarioDispositivos { get; set; }
        public ICollection<InventarioItem> InventarioItems { get; set; }
        




        public override bool EhValido()
        {            
            ValidacaoResultado = new InventarioValidator().Validate(this);
            foreach (var dispositivo in InventarioDispositivos)
            {
                var dispositivoValido = dispositivo.EhValido();
                if (!dispositivoValido)
                {
                    ValidacaoResultado.Errors.AddRange(dispositivo.ValidacaoResultado.Errors);
                }
            }
            foreach (var item in InventarioItems)
            {
                var itemValido = item.EhValido();
                if (!itemValido)
                {
                    ValidacaoResultado.Errors.AddRange(item.ValidacaoResultado.Errors);
                }
            }
            return ValidacaoResultado.IsValid;
        }

        public static string InventarioIdInvalido => "InventarioId em esta invalido!";
        public static string IniciadoEmInvalido => "Inciado em esta invalido!";


        public class InventarioValidator : AbstractValidator<Inventario>
        {
            public InventarioValidator()
            {
                ValidarIniciadoEm();

            }

            private void ValidarIniciadoEm()
            {
                RuleFor(x => x.IniciadoEm)
                    .NotEmpty()
                    .WithMessage(IniciadoEmInvalido);
            }

          
        }
    }
}

