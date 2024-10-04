using AutoMapper;
using ride_wise_api.Application.Models;
using ride_wise_api.Domain.Models;

namespace ride_wise_api.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MotorcycleResult, Motorcycle>();
            CreateMap<Motorcycle, MotorcycleRequest>();
            CreateMap<Motorcycle, MotorcycleResult>();
            CreateMap<Motorcycle, MotorcycleLicensePlate>();
            CreateMap<MotorcycleRequest, Motorcycle>();            
        }
    }
}
