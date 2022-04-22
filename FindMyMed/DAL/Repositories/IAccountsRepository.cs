using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IAccountsRepository
    {
        bool CreateAccount(Account account);
        Account GetAccountById(int id);
        Account GetAccount(LoginAccount loginAccount);
        IEnumerable<Account> GetAccounts();
        UpdateAccountDTO DeactivateAccount(int id, UpdateAccountDTO account);

    }
}
