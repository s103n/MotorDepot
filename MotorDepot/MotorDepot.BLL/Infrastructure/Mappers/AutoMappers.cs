using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using AutoStatus = MotorDepot.BLL.Infrastructure.Enums.AutoStatus;
using AutoType = MotorDepot.BLL.Infrastructure.Enums.AutoType;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class AutoMappers
    {
        public static AutoDto ToAutoDto(this Auto auto)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Auto, AutoDto>()
                    .ForMember("AutoBrand", opt => opt.MapFrom(x => x.Brand.Name))
                    .ForMember("Type", opt => opt.MapFrom(x => (AutoType)x.AutoTypeId))
                    .ForMember("Status", opt => opt.MapFrom(x => (AutoStatus)x.StatusId))
                    .ForMember("Flights", opt => opt.MapFrom(x => x.Flights.ToFlightDtos()));
            }).CreateMapper().Map<Auto, AutoDto>(auto);
        }
    }
}
