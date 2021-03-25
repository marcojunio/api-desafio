using backend.domain.Entities;
using backend.domain.Interfaces.Repository;
using backend.infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_repository.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _dbContext;
        
        public PersonRepository(PersonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Person entity)
        {
            _dbContext.People.Add(entity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Person> All()
        {
            var people = _dbContext.People.ToList();

            return people;
        }

        public void Delete(int code)
        {
            var person = _dbContext.People.Where(x => x.Code == code).FirstOrDefault();

            _dbContext.People.Remove(person);
            _dbContext.SaveChanges();
        }

        public Person FindByCode(int code)
        {
            var person = _dbContext.People.Where(x => x.Code == code).FirstOrDefault();

            return person;
        }

        public void Update(Person entity,int code)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        
        public IEnumerable<Person> GetPeopleFromState(string uf)
        {
            var people = _dbContext.People.Where(x => x.Uf.Contains(uf)).ToList();

            return people;
        }
        
        public bool CodeExits(int code)
        {
            return _dbContext.People.Any(x => x.Code == code);
        }
    }
}
