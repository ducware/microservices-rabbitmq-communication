using AutoMapper;
using MSE.Common.Models;
using MSE.User.Application.Commands.UserAccounts;
using MSE.User.Application.Commands.Users;
using MSE.User.Domain.Entities;
using MSE.User.Domain.Models;

namespace MSE.User.Application.Configurations
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateUserCommand, Domain.Entities.User>();
            CreateMap<CreateUserAccountCommand, UserAccount>();
            CreateMap<UserAccount, UserAccountDto>();
            CreateMap<Domain.Entities.User, UserInfoDto>();
        }
    }
}
