using FluentValidation;
using RideWise.Api.Application.Models;

namespace RideWise.Api.Application.Validators
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
              .Must(o => BeAValidDriverLicenseType(o)).WithMessage("{\"mensagem\": \"Dados inválidos\"}");
            RuleFor(o => o.Image_cnh)
              .NotEmpty().WithMessage("{\"mensagem\": \"Dados inválidos\"}")
              .NotNull().WithMessage("{\"mensagem\": \"Dados inválidos\"}")
              .Must(o => BeValidBase64String(o)).WithMessage("{\"mensagem\": \"Dados inválidos\"}");
        }
        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
        private bool BeAValidDriverLicenseType(string diverLicenseType)
        {
            var validTypes = new String[] { "A", "B", "AB" };
            return validTypes.Contains(diverLicenseType);
        }
        private bool BeValidBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }
    }
}
