using FluentValidation;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Enums;

namespace ride_wise_api.Application.Validators
{
    public class DeliveryAgentRequestValidator : AbstractValidator<DeliveryAgentRequest>
    {
        public DeliveryAgentRequestValidator()
        {
            RuleFor(o => o.Identificador)
                .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Nome)
                .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Cnpj)
               .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Data_nascimento)
               .Must(o => BeAValidDate(o)).WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Numero_cnh)
              .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Tipo_cnh)              
              .IsInEnum().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Image_cnh)
              .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
