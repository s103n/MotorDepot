using AutoMapper;
using MotorDepot.BLL.Models;
using MotorDepot.WEB.Models;

namespace MotorDepot.WEB.Infrastructure.Mappers
{
    public static class AccountMappers
    {
        public static UserDto ToUserDto(this RegisterViewModel rvm, string role)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterViewModel, UserDto>()
                    .ForMember("Role", opt => opt.MapFrom(x => role));
            }).CreateMapper().Map<RegisterViewModel, UserDto>(rvm);
        }

        public static UserDto ToUserDto(this DispatcherRegisterViewModel model)
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DispatcherRegisterViewModel, UserDto>()
                    .ForMember("Role", opt => opt.MapFrom(x => "dispatcher"));
            }).CreateMapper().Map<DispatcherRegisterViewModel, UserDto>(model);
        }
    }
}