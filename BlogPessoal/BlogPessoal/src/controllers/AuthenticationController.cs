using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Atributos
        private readonly IAuthentication _services;
        #endregion

        #region Construtores
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Responsible for getting the authorization
        /// </summary>
        /// <param name="authentication">AuthenticateDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Requisition example:
        ///
        ///     POST /api/Authentication
        ///     {
        ///        "email": "cleiton@email.com",
        ///        "senha": "123984"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return Created User</response>
        /// <response code="400">Requisition Error</response>
        /// <response code="401">E-mail or password invalid</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorizationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
#endregion