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
    public class UserController : ControllerBase
    {
        private UserRepository userRepository;
        private readonly PersonDbContext dbContext;
        public UserController(PersonDbContext context)
        {
            dbContext = context;
            userRepository = new UserRepository(dbContext);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = userRepository.Get(model.Password, model.Username);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("register")]
        public ActionResult<Person> RegisterUser([FromBody] User user)
        {
            
            if(user != null)
            {
                userRepository.Add(user);
                return Ok();
            }

            return NoContent();
        }
    }
}
