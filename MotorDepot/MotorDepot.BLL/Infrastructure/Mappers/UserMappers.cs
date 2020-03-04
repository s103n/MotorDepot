using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class UserMappers
    {
        public static AppUser ToEntity(this UserDto userDto)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, AppUser>()
                .ForMember("UserName", opt => opt.MapFrom(x => userDto.Email));
            }).CreateMapper().Map<UserDto, AppUser>(userDto);
        }

        public static UserDto ToUserDto(this AppUser user)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDto>())
                .CreateMapper().Map<AppUser, UserDto>(user);
        }

        public static IEnumerable<UserDto> ToDto(this IEnumerable<AppUser> appUsers)
        {
            return appUsers.Select(x => x.ToUserDto());
        }

        public static DriverDto ToDriverDto(this AppUser user)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<AppUser, DriverDto>())
                .CreateMapper().Map<AppUser, DriverDto>(user);
        }

        public static DispatcherDto ToDispatcherDto(this AppUser user)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<AppUser, DispatcherDto>())
                .CreateMapper().Map<AppUser, DispatcherDto>(user);
        }

        public static AppUser ToEntity(this DriverDto driver)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DriverDto, AppUser>();
                })
                .CreateMapper().Map<DriverDto, AppUser>(driver);
        }

        public static AppUser ToEntity(this DispatcherDto dispatcher)
        {
            return new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DispatcherDto, AppUser>();
                })
                .CreateMapper().Map<DispatcherDto, AppUser>(dispatcher);
        }
    }
}
