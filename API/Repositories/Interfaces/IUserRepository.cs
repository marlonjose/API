using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Save(User user);
        User FindUserLogin(string name, string password);
    }
}
