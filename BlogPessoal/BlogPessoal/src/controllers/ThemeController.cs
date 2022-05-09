using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using BlogPessoal.src.dtos;

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

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            var list = _repository.GetAllThemes();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);

            if (theme == null) return NotFound();

            return Ok(theme);
        }

        [HttpGet("search")]
        public IActionResult GetThemeByDescription([FromQuery] string descriptiontheme)
        {
            var themes = _repository.GetThemeByDescription(descriptiontheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);

        }

        [HttpPost]
        public IActionResult NewTheme([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewTheme(theme);
            
            return Created($"api/Themes", theme);

        }

        [HttpPut]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO uptheme)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateTheme(uptheme);

            return Ok(uptheme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int delTheme)
        {
            _repository.DeleteTheme(delTheme);
            return NoContent();
        }

        #endregion

    }
}
