using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Bernal.ERP.ControleInventario.Dominio.Entidades.Inventario;

namespace Bernal.ERP.ControleInventario.Dominio.Entidades
{
    public class InventarioItem : Entidade
    {       
        public string ProdutoCodigo { get; set; }
        public string ProdutoNome { get; set; }
        public int Embalagem { get; set; }
        public decimal QuantidadeDeEmbalagem { get; set; }
        
        public int InventarioItemId { get; set; }
        public decimal Precocustoembalagem { get; set; }
        public decimal Precofinal { get; set; }
        public decimal Icms { get; set; }
        public decimal Transporte { get; set; }
        public DateTime? ExcluidoEm { get; set; }
        public decimal QtantidadeTotal => QuantidadeDeEmbalagem * Embalagem;
        public decimal PrecoCustoTotal => Precocustoembalagem * QuantidadeDeEmbalagem;
        public decimal PrecoFinalTotal => Precofinal * QuantidadeDeEmbalagem;
        public decimal Lucro => (Precofinal*QuantidadeDeEmbalagem) - ((Precocustoembalagem * QuantidadeDeEmbalagem) + Icms + Transporte);
        public ICollection<Contagem> Contagem { get; set; }
        
        public int InventarioId { get; set; }

        public  Inventario Inventario { get; set; }

        public override bool EhValido()
        {
            ValidacaoResultado = new InventarioItemValidator().Validate(this);
            return ValidacaoResultado.IsValid;
        }
    }
    
    public class InventarioItemValidator : AbstractValidator<InventarioItem>
    {
        public InventarioItemValidator()
        {

            ProdutoCodigoValidator();
            ProdutoNomeValidator();
            EmbalagemValidator();
            QuantidadeEmbalagemValidator();
            Precodecustoembalagemvalidador();
            Precofinalvalidador();
            Icmsvalidador();
            TransporteValidador();
        }

        private void Precodecustoembalagemvalidador()
        {
            RuleFor(x => x.Precocustoembalagem)
                               .NotEmpty()
                               .WithMessage("Preco de custo embalagem está invalido!");
        }
        private void Precofinalvalidador()
        {
            RuleFor(x => x.Precofinal)
                               .NotEmpty()
                               .WithMessage("Preco final está invalido!");
        }

        private void Icmsvalidador()
        {
            RuleFor(x => x.Icms)
                               .NotEmpty()
                               .WithMessage("Preco ICMS está invalido!");
        }

        private void TransporteValidador()
        {
            RuleFor(x => x.Transporte)
                               .NotEmpty()
                               .WithMessage("Valor do transporte está invalido!");
        }

        private void QuantidadeEmbalagemValidator()
        {
            RuleFor(x => x.QuantidadeDeEmbalagem)
                                .NotEmpty()
                                .WithMessage("Quantidade de embalagem está invalida!");
        }

        private void EmbalagemValidator()
        {
            RuleFor(x => x.Embalagem)
                                .NotEmpty()
                                .WithMessage("Embalagem está invalida!");
        }

        private void ProdutoNomeValidator()
        {
            RuleFor(x => x.ProdutoNome)
                                .NotEmpty()
                                .WithMessage("Produto nome está invalida!");
        }

        private void ProdutoCodigoValidator()
        {
            RuleFor(x => x.ProdutoCodigo)
                    .NotEmpty()
                    .WithMessage("Produto codigo está invalida!");
        }

    }
}
