﻿using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IAccountsRepository
    {
        bool CreateAccount(Account account);
        Account GetAccountById(int id);
        IEnumerable<Account> GetAccounts();

    }
}