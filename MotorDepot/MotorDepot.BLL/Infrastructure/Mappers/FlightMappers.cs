using System;
using System.Collections.Generic;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;
using FlightStatus = MotorDepot.BLL.Infrastructure.Enums.FlightStatus;

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
                        .ForMember("Status", opt => opt.MapFrom(x => (FlightStatus) x.StatusId))
                        .ForMember("Auto", opt => opt.MapFrom(x => x.Auto.ToAutoDto()))
                        .ForMember("Driver", opt => opt.MapFrom(x => x.Driver.ToDriverDto()));
                })
                .CreateMapper()
                .Map<IEnumerable<Flight>, IEnumerable<FlightDto>>(models);
        }

        public static FlightRequest ToFlightRequest(this FlightRequestDto request)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlightRequestDto, FlightRequest>()
                    .ForMember("Driver", opt => opt.Ignore())
                    .ForMember("Dispatcher", opt => opt.Ignore())
                    .ForMember("Status", opt => opt.Ignore())
                    .ForMember("AutoType", opt => opt.Ignore())
                    .ForMember("DriverId", opt => opt.MapFrom(x => request.Driver.Id))
                    .ForMember("DispatcherId", opt => opt.MapFrom(x => request.Dispatcher.Id))
                    .ForMember("FlightRequestStatusId", opt => opt.MapFrom(x => (int) request.Status))
                    .ForMember("FlightId", opt => opt.MapFrom(x => request.RequestedFlight.Id))
                    .ForMember("AutoTypeId", opt => opt.MapFrom(x => (int) request.AutoType));
            }).CreateMapper().Map<FlightRequestDto, FlightRequest>(request);
        }
    }
}
