using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static FlightViewModel ToFlightVm(this FlightDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDto, FlightViewModel>()
                    .ForMember("Status", opt => opt.MapFrom(model.Status.Name))
                    .ForMember("StatusColor", opt => opt.MapFrom(model.Status.Color))
                    .ForMember("AutoName", opt => opt.MapFrom(model.Auto.Name))
                    .ForMember("AutoNumber", opt => opt.MapFrom(model.Auto.Number))
                    .ForMember("DriverEmail", opt => opt.MapFrom(model.Driver.Email))
                    .ForMember("DriverName", opt => opt.MapFrom($"{model.Driver.FirstName} {model.Driver.LastName}"));
            }).CreateMapper().Map<FlightDto, FlightViewModel>(model);
        }
    }
}