using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Enums;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Mappings
{
    public class DeliveryAgentResultMapper : Profile
    {
        public DeliveryAgentResultMapper()
        {
            _ = CreateMap<DeliveryAgent, DeliveryAgentResult>()
                .ForMember(f => f.Identificador, o => o.MapFrom((source, destination, member, context) => source.Identification))
                .ForMember(f => f.Nome, o => o.MapFrom((source, destination, member, context) => source.Name))
                .ForMember(f => f.Cnpj, o => o.MapFrom((source, destination, member, context) => source.IdentificationDocument))
                .ForMember(f => f.Data_nascimento, o => o.MapFrom((source, destination, member, context) => source.BirthDate))
                .ForMember(f => f.Numero_cnh, o => o.MapFrom((source, destination, member, context) => source.DriverLicenseNumber))
                .ForMember(f => f.Tipo_cnh, o => o.MapFrom((source, destination, member, context) => source.DriverLicenseType));
        }
    }
}
