using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public class Profiles : AutoMapper.Profile
    {
        public Profiles()
        {
            CreateMap<Account, ReadAccountDTO>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<Account, UpdateAccountDTO>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<User, UpdateUserDTO>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
