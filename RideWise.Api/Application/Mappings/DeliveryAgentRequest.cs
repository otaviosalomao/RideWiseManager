using AutoMapper;
using RideWise.Api.Application.Models;
using RideWise.Api.Domain.Models;

namespace RideWise.Api.Application.Mappings
{
    public class DeliveryAgentRequestMapper : Profile
    {
        public DeliveryAgentRequestMapper() 
        {
            _ = CreateMap<DeliveryAgentRequest, DeliveryAgent>()
                .ForMember(f => f.Identification, o => o.MapFrom((source, destination, member, context) => source.Identificador))
                .ForMember(f => f.Name, o => o.MapFrom((source, destination, member, context) => source.Nome))
                .ForMember(f => f.IdentificationDocument, o => o.MapFrom((source, destination, member, context) => source.Cnpj))
                .ForMember(f => f.BirthDate, o => o.MapFrom((source, destination, member, context) => source.Data_nascimento))
                .ForMember(f => f.DriverLicenseNumber, o => o.MapFrom((source, destination, member, context) => source.Numero_cnh))
                .ForMember(f => f.DriverLicenseType, o => o.MapFrom((source, destination, member, context) => source.Tipo_cnh));
        }
    }
}
