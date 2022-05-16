using FluentValidation.Results;

namespace Bernal.ERP.ControleInventario.Dominio.Entidades
{
    public abstract class Entidade
    {
        public ValidationResult ValidacaoResultado { get;  set; }

        public abstract bool EhValido();
    }
}
