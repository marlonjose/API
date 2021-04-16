using API.Data;
using API.Models;
using API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Save(User user)
        {
            _context.Users.Add(user);
        }

        public User FindUserLogin(string name, string password)
        {
            return _context.Users.Where(u => u.name == name && u.Password == password).FirstOrDefault();
        }
    }
}
