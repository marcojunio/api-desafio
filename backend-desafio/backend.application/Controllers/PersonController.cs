using backend.domain.Entities;
using backend.infra.Context;
using backend_repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.application.Controllers
{
    [Route("api/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private  PersonRepository personRepository;
        private readonly PersonDbContext dbContext;

        public PersonController(PersonDbContext context)
        {
            dbContext = context;
            personRepository = new PersonRepository(dbContext);
        }

        [HttpPost]
        public ActionResult<Person> InsertPerson(Person person)
        {
            if (person == null || person.isValid().Equals(false))
            {
                return BadRequest();
            }

            personRepository.Add(person);

            return Ok(person);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            var people = personRepository.All();

            return Ok(people);
        }

        [HttpGet("{code}")]
        public ActionResult<Person> GetPersonByCode(int code)
        {
            var people = personRepository.FindByCode(code);

            if(people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpGet("state/{uf}")]
        public ActionResult<IEnumerable<Person>> GetPersonByState(string uf)
        {
            var people = personRepository.GetPeopleFromState(uf);

            if(people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpDelete("{code}")]
        public ActionResult<Person> DeletePerson(int code)
        {
            personRepository.Delete(code);

            return NoContent();
        }

        [HttpPut("{code}")]
        public ActionResult<Person> UpdatePerson(Person person, int code)
        {
            if(code != person.Code)
            {
                return BadRequest();
            }

            if (!personRepository.CodeExits(code)) 
            {
                return NotFound();
            }

            personRepository.Update(person, code);

            return Ok(person);
        }
    }
}
