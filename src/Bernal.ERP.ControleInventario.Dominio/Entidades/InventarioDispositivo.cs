
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Bernal.ERP.ControleInventario.Dominio.Entidades.Inventario;

namespace Bernal.ERP.ControleInventario.Dominio.Entidades
{
    public class InventarioDispositivo: Entidade
    {
        
        public string NumeroDeSerie { get; set; }
        public DateTime IniciadoEm { get; set; } =  DateTime.Now;
        public DateTime? FinalizadoEm { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public string ResponsavelPelaContagem { get; set; }
        public int InventarioId { get; set; }
        public int InventarioDispositivoId { get; set; }
        
        public Inventario Inventario { get; set; }








        public override bool EhValido()
        {
            ValidacaoResultado = new InventarioDispositivoValidator().Validate(this);
            return ValidacaoResultado.IsValid;
        }

        public static string IniciadoEmInvalido => "Inciado em esta invalido!";
        public static string NumeroDeSerieEmInvalido => "Numero de serie esta invalido!";
        public static string InventarioDispositivoIdInvalido => "InventarioDispositivo esta invalido!";
        public static string ResponsavelPelaContagemIdInvalido => "ResponsavelPelaContagem esta invalido!";
        public static string InventarioIdInvalido => "InventarioId esta invalido!";


       public class InventarioDispositivoValidator : AbstractValidator<InventarioDispositivo>
        {
            public InventarioDispositivoValidator()
            {
                ValidarIniciadoEm();
                ValidarNumeroDeSerie();
                ValidarResponsavelPelaContagem();
 
            }

           

            private void ValidarIniciadoEm()
            {
                RuleFor(x => x.IniciadoEm)
                    .NotEmpty()
                    .WithMessage(IniciadoEmInvalido);
            }

            private void ValidarNumeroDeSerie()
            {
                RuleFor(x => x.NumeroDeSerie)
                    .NotEmpty()
                    .WithMessage(NumeroDeSerieEmInvalido);
            }
            private void ValidarResponsavelPelaContagem()
            {
                RuleFor(x => x.ResponsavelPelaContagem)
                    .NotEmpty()
                    .WithMessage(ResponsavelPelaContagemIdInvalido);
            }
          
        }
    }
}
