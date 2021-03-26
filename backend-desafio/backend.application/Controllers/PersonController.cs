using backend.application.Services;
using backend.domain.Entities;
using backend.infra.Context;
using backend_repository.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly PersonRepository personRepository;
        private readonly PersonDbContext dbContext;

        public PersonController(PersonDbContext context)
        {
            dbContext = context;
            personRepository = new PersonRepository(dbContext);
        }

        [HttpPost]
        [Authorize]
        public ActionResult<Person> InsertPerson(Person person)
        {
            if (person == null || person.isValid().Equals(false))
                return BadRequest();

            personRepository.Add(person);

            return Ok(person);
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Person>> GetAllPeople()
        {
            var people = personRepository.All();

            return Ok(people);
        }

        [HttpGet("{code}")]
        [Authorize]
        public ActionResult<Person> GetPersonByCode(int code)
        {
            var people = personRepository.FindByCode(code);

            if(people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpGet("state/{uf}")]
        [Authorize]
        public ActionResult<IEnumerable<Person>> GetPersonByState(string uf)
        {
            var people = personRepository.GetPeopleFromState(uf);

            if(people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpDelete("{code}")]
        [Authorize]
        public ActionResult<Person> DeletePerson(int code)
        {
            personRepository.Delete(code);

            return NoContent();
        }

        [HttpPut("{code}")]
        [Authorize]
        public ActionResult<Person> UpdatePerson(Person person, int code)
        {
            if(code != person.Code)
                return BadRequest();

            if (!personRepository.CodeExits(code)) 
                return NotFound();

            personRepository.Update(person, code);

            return Ok(person);
        }
    }
}
