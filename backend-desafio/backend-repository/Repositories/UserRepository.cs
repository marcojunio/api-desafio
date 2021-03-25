using backend.domain.Entities;
using backend.infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_repository.Repositories
{
    public class UserRepository
    {
        private readonly PersonDbContext _dbContext;

        public UserRepository(PersonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public User Get(string password,string username)
        {
           var user =  _dbContext.Users.Where(x => x.Username == username && x.Password == password).First();

           return user;
        }

    }
}
