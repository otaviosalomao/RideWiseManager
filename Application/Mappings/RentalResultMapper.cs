using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Mappings
{
    public class RentalResultMapper : Profile
    {
        public RentalResultMapper()
        {
            _ = CreateMap<Rental, RentalResult>()
                .ForMember(f => f.Entregador_id, o => o.MapFrom((source, destination, member, context) => source.DeliveryAgentIdentification))
                .ForMember(f => f.Moto_id, o => o.MapFrom((source, destination, member, context) => source.MotorcycleIdentification))
                .ForMember(f => f.Data_inicio, o => o.MapFrom((source, destination, member, context) => source.StartDate))
                .ForMember(f => f.Data_termino, o => o.MapFrom((source, destination, member, context) => source.EndDate))
                .ForMember(f => f.Data_previsao_termino, o => o.MapFrom((source, destination, member, context) => source.EstimatedEndDate))                
                .ForMember(f => f.Valor_diaria, o => o.MapFrom((source, destination, member, context) => source.DailyValue));
        }
    }
}
