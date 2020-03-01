using System;
using System.Collections.Generic;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class FlightMappers
    {
        public static Flight ToFlight(this FlightDto model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<FlightDto, Flight>())
                .CreateMapper()
                .Map<FlightDto, Flight>(model);
        }

        public static FlightDto ToFlightDto(this Flight model)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<Flight, FlightDto>())
                .CreateMapper()
                .Map<Flight, FlightDto>(model);
        }

        public static IEnumerable<FlightDto> ToFlightDtos(this IEnumerable<Flight> models)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Flight, FlightDto>()
                        .ForMember("Auto", opt => opt.MapFrom(x => x.Auto))
                })
                .CreateMapper()
                .Map<IEnumerable<Flight>, IEnumerable<FlightDto>>(models);
        }

        public static FlightStatusDto ToFlightStatusDto(this FlightStatus status)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<FlightStatus, FlightStatusDto>())
                .CreateMapper().Map<FlightStatus, FlightStatusDto>(status);
        }

        public static FlightRequest ToFlightRequest(this FlightRequestDto request)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestDto, FlightRequest>()
                    //.ForMember("DriverId", opt => opt.MapFrom(x => request.Driver.Id))
                    //.ForMember("DispatcherId", opt => opt.MapFrom(x => request.Dispatcher.Id))
                    .ForMember("FlightRequestStatusId", opt => opt.MapFrom(x => (int) request.Status))
                    .ForMember("FlightId", opt => opt.MapFrom(x => request.RequestedFlight.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int) request.AutoType));
            }).CreateMapper().Map<FlightRequestDto, FlightRequest>(request);
        }
    }
}
