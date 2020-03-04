using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class AccountMappers
    {
        public static UserDto ToUserDto(this UserRegisterViewModel rvm, string role)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegisterViewModel, UserDto>()
                    .ForMember("Role", opt => opt.MapFrom(x => role));
            }).CreateMapper().Map<UserRegisterViewModel, UserDto>(rvm);
        }

        public static UserDto ToUserDto(this LoginViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LoginViewModel, UserDto>(); 

            }).CreateMapper().Map<LoginViewModel, UserDto>(model);
        }

        public static DriverDto ToDriverDto(this UserDto user, string id)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DriverDto>()
                    .ForMember("Id", opt => opt.MapFrom(x => id));
            }).CreateMapper().Map<UserDto, DriverDto>(user);
        }

        public static DispatcherDto ToDispatcherDto(this UserDto user, string id)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, DispatcherDto>()
                    .ForMember("Id", opt => opt.MapFrom(x => id));
            }).CreateMapper().Map<UserDto, DispatcherDto>(user);
        }
    }
}