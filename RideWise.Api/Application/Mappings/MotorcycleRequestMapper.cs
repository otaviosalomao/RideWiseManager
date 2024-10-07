using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Mappings
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
