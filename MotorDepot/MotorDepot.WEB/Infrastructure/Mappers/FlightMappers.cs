using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;
using WebGrease.Css.Extensions;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static FlightViewModel ToFlightVm(this FlightDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDto, FlightViewModel>()
                    .ForMember("Status", opt => opt.MapFrom(x => model.Status.Name))
                    .ForMember("StatusColor", opt => opt.MapFrom(x => model.Status.Color))
                    .ForMember("AutoName", opt => opt.MapFrom(x => model.Auto.Model))
                    .ForMember("AutoNumber", opt => opt.MapFrom(x => model.Auto.Numbers))
                    .ForMember("DriverEmail", opt => opt.MapFrom(x => model.Driver.Email))
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{model.Driver.FirstName} {model.Driver.LastName}"));
            }).CreateMapper().Map<FlightDto, FlightViewModel>(model);
        }

        public static IEnumerable<FlightViewModel> ToFlightVm(this IEnumerable<FlightDto> models)
        {
            return models.Select(x => x.ToFlightVm());
        }

        public static FlightRequestDto ToFlightRequestDto(this FlightRequestViewModel model,
            UserDto driver, UserDto dispatcher, FlightDto flight)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestViewModel, FlightRequestDto>()
                    .ForMember("Driver", opt => opt.MapFrom(x => driver))
                    .ForMember("Dispatcher", opt => opt.MapFrom(x => dispatcher))
                    .ForMember("RequestedFlight", opt => opt.MapFrom(x => flight));
            }).CreateMapper().Map<FlightRequestViewModel, FlightRequestDto>(model);
        }
    }
}