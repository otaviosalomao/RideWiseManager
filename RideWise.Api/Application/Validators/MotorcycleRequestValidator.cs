using FluentValidation;
using RideWise.Api.Application.Models;

namespace RideWise.Api.Application.Validators
{
    public class MotorcycleRequestValidator : AbstractValidator<MotorcycleRequest>
    {
        public MotorcycleRequestValidator()
        {
            RuleFor(o => o.Ano)
                .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Identificador)
                .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Modelo)
               .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Placa)
               .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
        }
    }
}
