﻿using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IUsersRepository
    {
        User GetUserById(int id);
        User GetUserByEmail(string email);
        IEnumerable<User> GetUsers();
        UpdateUserDTO UpdateUserProfile(int id, UpdateUserDTO userDTO);

    }
}
