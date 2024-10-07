using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Mappings
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
