using System.Collections.Generic;
using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.DAL.Entities;

namespace MotorDepot.BLL.Infrastructure.Mappers
{
    public static class UserMappers
    {

        private static readonly MapperConfiguration AppUserToUserDto = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AppUser, UserDto>()
                    .ForMember("Role", opt => opt.MapFrom(x => ""));
            });

        public static AppUser ToAppUser(this UserDto userDto)
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

        public static IEnumerable<UserDto> ToUserDtos(this IEnumerable<AppUser> appUsers)
        {
            return AppUserToUserDto.CreateMapper()
                .Map<IEnumerable<AppUser>, IEnumerable<UserDto>>(appUsers);
        }
    }
}
