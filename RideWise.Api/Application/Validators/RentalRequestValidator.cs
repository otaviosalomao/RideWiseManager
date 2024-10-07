using FluentValidation;
using RideWise.Api.Application.Models;
using RideWise.Api.Application.Repositories.Interfaces;
using RideWise.Api.Domain.Services.Interfaces;

namespace RideWise.Api.Application.Validators
{
    public class RentalRequestValidator : AbstractValidator<RentalRequest>
    {
        private readonly IRentService _rentService;
        private readonly IRepositoryManager _repositoryManager;

        public RentalRequestValidator(IRentService rentService, IRepositoryManager repositoryManager)
        {
            _rentService = rentService;
            _repositoryManager = repositoryManager;
        }

        public RentalRequestValidator()
        {
            var startRentDate = _rentService.StartRentDate(DateTime.Now);
            RuleFor(x => x.Data_inicio)
                .Must(o => o == startRentDate)
                .WithMessage("{\"mensagem\": \"Data de inicio inválida\"}");
            RuleFor(x => x.Data_termino)
                .Must(o => o == startRentDate)
                .WithMessage("{\"mensagem\": \"Data de inicio inválida\"}");
            RuleFor(x => new { x.Data_previsao_termino, x.Plano })
                .Must(o => o.Data_previsao_termino == _rentService.EstimateEndRentDate(startRentDate, o.Plano))
                .WithMessage("{\"mensagem\": \"Data de previsao do termino inválida\"}");
            RuleFor(x => new { x.Data_termino, x.Plano })
                .Must(o => o.Data_termino == _rentService.EstimateEndRentDate(startRentDate, o.Plano))
                .WithMessage("{\"mensagem\": \"Data de termino inválida\"}");
            RuleFor(o => o.Plano)
                .Must(o => _rentService.ValidPlan(o)).WithMessage("{\"mensagem\": \"Plano invalido\"}");
        }       
    }
}
