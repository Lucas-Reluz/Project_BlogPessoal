using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using BlogPessoal.src.dtos;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BlogPessoal.src.models;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Atributos

        private readonly ITheme _repository;

        #endregion


        #region Construtores

        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }

        #endregion


        #region Metodos

        /// <summary>
        /// Get all themes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">list of themes</response>
        /// <response code="204">empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repository.GetAllThemesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Get theme by id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return theme</response>
        /// <response code="404">Theme does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        /// <summary>
        /// Get theme by description
        /// </summary>
        /// <param name="descriptiontheme">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return themes</response>
        /// <response code="204">Description does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptiontheme)
        {
            var themes = await  _repository.GetThemeByDescriptionAsync(descriptiontheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);

        }

        /// <summary>
        /// Create new theme
        /// </summary>
        /// <param name="theme">NewThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Requisition example:
        ///
        ///     POST /api/Themes
        ///     {
        ///         "description" : "Biologia"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created theme</response>
        /// <response code="400">requisition error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewThemeAsync([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewThemeAsync(theme);
            
            return Created($"api/Themes", theme);

        }

        /// <summary>
        /// Update theme
        /// </summary>
        /// <param name="uptheme">NewThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Requisition example:
        ///
        ///     POST /api/Themes
        ///     {
        ///         "description" : "Matematica"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return updated theme</response>
        /// <response code="400">requisition error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO uptheme)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateThemeAsync(uptheme);

            return Ok(uptheme);
        }

        /// <summary>
        /// Delete theme by id
        /// </summary>
        /// <param name="delTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Deleted theme</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int delTheme)
        {
           await _repository.DeleteThemeAsync(delTheme);
            return NoContent();
        }

        #endregion

    }
}
