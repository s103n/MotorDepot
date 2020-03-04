using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;
using System.Collections.Generic;
using System.Linq;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static FlightViewModel ToDisplayViewModel(this FlightDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightDto, FlightViewModel>()
                    .ForMember("AutoName", opt => opt.MapFrom(x => x.Auto.Model))
                    .ForMember("AutoNumbers", opt => opt.MapFrom(x => x.Auto.Numbers))
                    .ForMember("DriverEmail", opt => opt.MapFrom(x => x.Driver.Email))
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"));
            }).CreateMapper().Map<FlightDto, FlightViewModel>(model);
        }

        public static IEnumerable<FlightViewModel> ToDisplayViewModel(this IEnumerable<FlightDto> models)
        {
            return models.Select(x => x.ToDisplayViewModel());
        }

        public static FlightRequestDto ToDto(this FlightRequestCreateViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestCreateViewModel, FlightRequestDto>()
                    .ForMember("Driver", opt => opt.MapFrom(x => new DriverDto { Id = model.DriverId }))
                    .ForMember("Dispatcher", opt => opt.MapFrom(x => new DispatcherDto()))
                    .ForMember("RequestedFlight", opt => opt.MapFrom(x => new FlightDto { Id = model.RequestedFlightId }));
            }).CreateMapper().Map<FlightRequestCreateViewModel, FlightRequestDto>(model);
        }

        public static FlightRequestDisplayViewModel ToDisplayViewModel(this FlightRequestDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestDto, FlightRequestDisplayViewModel>()
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"));
            }).CreateMapper().Map<FlightRequestDto, FlightRequestDisplayViewModel>(model);
        }

        public static IEnumerable<FlightRequestDisplayViewModel> ToDisplayViewModels(
            this IEnumerable<FlightRequestDto> models)
        {
            return models.Select(x => x.ToDisplayViewModel());
        }

        public static FlightRequestDetailsViewModel ToDetailsViewModel(this FlightRequestDto model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestDto, FlightRequestDetailsViewModel>()
                    .ForMember("DriverName", opt => opt.MapFrom(x => $"{x.Driver.FirstName} {x.Driver.LastName}"))
                    .ForMember("DriverEmail", opt => opt.MapFrom(x => x.Driver.Email));
            }).CreateMapper().Map<FlightRequestDto, FlightRequestDetailsViewModel>(model);
        }
    }
}