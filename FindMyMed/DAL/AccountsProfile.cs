using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public class AccountsProfile : AutoMapper.Profile
    {
        public AccountsProfile()
        {
            CreateMap<Account, ReadAccountDTO>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<Account, UpdateAccountDTO>();
            CreateMap<UpdateAccountDTO, Account>();
        }
    }
}
