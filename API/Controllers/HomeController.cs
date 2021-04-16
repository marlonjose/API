using API.Data.Interfaces;
using API.Models;
using API.Repositories.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    //[Route("home")]
    public class HomeController : Controller
    { 
        [HttpPost]
        [Route("SaveUser")]
        public User SaveUser([FromServices]IUserRepository userRepository, [FromServices] IUnitOfWork unitOfWork, [FromBody] User model)
        {

            try
            {
                userRepository.Save(model);

                unitOfWork.Commit();

                return model;
            }
            catch(Exception e)
            {
                unitOfWork.Rollback();
                return null;
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices] IUserRepository userRepository, [FromBody] User model)
        {
            // Recupera o usuário
            var user = userRepository.FindUserLogin(model.Username, model.Password);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format($"Autenticado", User.Identity.Name);

        [HttpGet]
        [Route("developer")]
        [Authorize(Roles = "Developer, Manager")]
        public string Developer() => "Developer";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Manager";
    }
}
