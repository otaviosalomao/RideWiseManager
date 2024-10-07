using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Mappings
{
    public class RentalRequestMapper : Profile
    {
        public RentalRequestMapper()
        {
            _ = CreateMap<RentalRequest, Rental>()
                .ForMember(f => f.DeliveryAgentIdentification, o => o.MapFrom((source, destination, member, context) => source.Entregador_id))
                .ForMember(f => f.MotorcycleIdentification, o => o.MapFrom((source, destination, member, context) => source.Moto_id))
                .ForMember(f => f.StartDate, o => o.MapFrom((source, destination, member, context) => source.Data_inicio))
                .ForMember(f => f.EndDate, o => o.MapFrom((source, destination, member, context) => source.Data_termino))
                .ForMember(f => f.EstimatedEndDate, o => o.MapFrom((source, destination, member, context) => source.Data_previsao_termino))
                .ForMember(f => f.PlanNumber, o => o.MapFrom((source, destination, member, context) => source.Plano))
                .ForMember(f => f.CreatedAt, o => o.MapFrom((source, destination, member, context) => DateTime.Now));
        }
    }
}
