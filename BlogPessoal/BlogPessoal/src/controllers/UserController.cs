using BlogPessoal.src.dtos;
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

        [HttpPost]
        public IActionResult NewUser([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewUser(user);
            return Created($"api/Users/{user.Email}", user);

        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateUser(user);
            return Ok();

        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }
    }
}
