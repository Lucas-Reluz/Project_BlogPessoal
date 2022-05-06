using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Atributos

        private readonly IUser _repository;

        #endregion

        #region Construtores

        public UserController(IUser repository)
        {
            _repository = repository;
        }

        #endregion

        [HttpGet("id/{idUsers}")]

        public IActionResult GetUserbyId([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]

        public IActionResult GetUserByName([FromQuery] string userName)
        {
            var users = _repository.GetUserByUsername(userName);
            
            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailuser}")]
        public IActionResult GetUserByEmail([FromRoute] string userEmail)
        {
            var user = _repository.GetUserByEmail(userEmail);

            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
