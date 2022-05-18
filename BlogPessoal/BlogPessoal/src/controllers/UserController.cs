using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Atributos

        private readonly IUser _repository;
        private readonly IAuthentication _services;

        #endregion

        #region Construtores

        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }

        #endregion

        #region Metodos
        
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return User</response>
        /// <response code="404">User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "USER, ADMIN")]
        public async Task<ActionResult> GetUserbyIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }
        
        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="userName">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o usuario</response>
        /// <response code="204">Nome não existe</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "USER, ADMIN")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string userName)
        {
            var users = await _repository.GetUserByUsernameAsync(userName);
            
            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="emailUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the user</response>
        /// <response code="204">Email does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "USER, ADMIN")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }


        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Requisition example:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Cleiton Benicio",
        ///        "email": "cleiton@email.com",
        ///        "password": "1235673",
        ///        "photo": "URLFOTO",
        ///        "type": "USER"
        ///     }
        ///
        /// </remarks>
        /// <response code="201"> Return created user</response>
        /// <response code="400"> request error </response>
        /// <response code="401">E-mail already registered</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.CreateUserWithoutDuplicatingAsync(user);
                return Created($"api/Users/email{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Requisition example:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "name": "Lucas Reluz",
        ///        "password": "134652",
        ///        "photo": "URLFOTO"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return updated user</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "USER,ADMIN")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);
            
            await _repository.UpdateUserAsync(user);
            return Ok();

        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">User deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }
    }
    #endregion
}