using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Mappings
{
    public class MotorcycleRequestMapper : Profile
    {
        public MotorcycleRequestMapper()
        {
            _ = CreateMap<MotorcycleRequest, Motorcycle>()
                .ForMember(f => f.Identification, o => o.MapFrom((source, destination, member, context) => source.Identificador))
                .ForMember(f => f.Year, o => o.MapFrom((source, destination, member, context) => source.Ano))
                .ForMember(f => f.Model, o => o.MapFrom((source, destination, member, context) => source.Modelo))
                .ForMember(f => f.LicensePlate, o => o.MapFrom((source, destination, member, context) => source.Placa));
        }
    }
}
