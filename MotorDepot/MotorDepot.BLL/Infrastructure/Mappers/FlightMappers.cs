using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static Flight ToEntity(this FlightDto model)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FlightDto, Flight>()
                        .ForMember("FlightStatusLookupId", opt => opt.MapFrom(x => x.Status))
                        .ForMember("Status", opt => opt.Ignore())
                        .ForMember("Auto", opt => opt.Ignore())
                        .ForMember("AutoId", opt => opt.MapFrom(x => x.Auto.Id))
                        .ForMember("Driver", opt => opt.Ignore())
                        .ForMember("DriverId", opt => opt.MapFrom(x => x.Driver.Id))
                        .ForMember("CreatorId", opt => opt.MapFrom(x => x.DispatcherCreator.Id));
                })
                .CreateMapper()
                .Map<FlightDto, Flight>(model);
        }

        public static FlightDto ToDto(this Flight model)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Flight, FlightDto>()
                        .ForMember("Status", opt => opt.MapFrom(x => x.FlightStatusLookupId))
                        .ForMember("Auto", opt => opt.MapFrom(x => x.Auto.ToDto()))
                        .ForMember("Driver", opt => opt.MapFrom(x => x.Driver.ToDriverDto()))
                        .ForMember("DispatcherCreator", opt => opt.MapFrom(x => x.Creator.ToDispatcherDto()));
                }).CreateMapper().Map<Flight, FlightDto>(model);
        }

        public static IEnumerable<FlightDto> ToDto(this IEnumerable<Flight> models)
        {
            return models.Select(x => x.ToDto());
        }

        public static FlightRequest ToEntity(this FlightRequestDto request)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestDto, FlightRequest>()
                    .ForMember("Driver", opt => opt.Ignore())
                    .ForMember("Dispatcher", opt => opt.Ignore())
                    .ForMember("Status", opt => opt.Ignore())
                    .ForMember("AutoType", opt => opt.Ignore())
                    .ForMember("RequestedFlight", opt => opt.Ignore())
                    .ForMember("DriverId", opt => opt.MapFrom(x => request.Driver.Id))
                    .ForMember("DispatcherId", opt => opt.MapFrom(x => request.Dispatcher.Id))
                    .ForMember("FlightRequestStatusLookupId", opt => opt.MapFrom(x => (int)request.Status))
                    .ForMember("FlightId", opt => opt.MapFrom(x => request.RequestedFlight.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int)request.AutoType));
            }).CreateMapper().Map<FlightRequestDto, FlightRequest>(request);
        }

        public static FlightRequestDto ToDto(this FlightRequest request)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequest, FlightRequestDto>()
                    .ForMember("Driver", opt => opt.MapFrom(x => x.Driver.ToDriverDto()))
                    .ForMember("Dispatcher", opt => opt.MapFrom(x => x.Dispatcher.ToDispatcherDto()))
                    .ForMember("Status", opt => opt.MapFrom(x => x.FlightRequestStatusLookupId))
                    .ForMember("RequestedFlight", opt => opt.MapFrom(x => x.RequestedFlight.ToDto()))
                    .ForMember("AutoType", opt => opt.MapFrom(x => x.AutoTypeId));
            }).CreateMapper().Map<FlightRequest, FlightRequestDto>(request);
        }

        public static IEnumerable<FlightRequestDto> ToDto(this IEnumerable<FlightRequest> requests)
        {
            return requests.Select(x => x.ToDto());
        }

        public static FlightDto ToEditWith(this FlightDto model, FlightDto other)
        {
            model.Description = other.Description;
            model.Status = other.Status;
            model.ArrivalPlace = other.ArrivalPlace;
            model.DeparturePlace = other.DeparturePlace;
            model.Distance = other.Distance;

            return model;
        }
    }
}
