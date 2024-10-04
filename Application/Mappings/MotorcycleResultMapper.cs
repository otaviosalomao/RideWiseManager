using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Mappings
{
    public class MotorcycleResultMapper : Profile
    {
        public MotorcycleResultMapper()
        {
            _ = CreateMap<Motorcycle, MotorcycleResult> ()
                .ForMember(f => f.Identificador, o => o.MapFrom((source, destination, member, context) => source.Identification))
                .ForMember(f => f.Ano, o => o.MapFrom((source, destination, member, context) => source.Year))
                .ForMember(f => f.Modelo, o => o.MapFrom((source, destination, member, context) => source.Model))
                .ForMember(f => f.Placa, o => o.MapFrom((source, destination, member, context) => source.LicensePlate));
        }
    }
}
