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
        /// Pegar todos temas
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Lista de temas</response>
        /// <response code="204">Lista vasia</response>
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
        /// Pegar tema pelo Id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna o tema</response>
        /// <response code="404">Tema não existente</response>
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
        /// Pegar tema pela Descrição
        /// </summary>
        /// <param name="descriptiontheme">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna temas</response>
        /// <response code="204">Descrição não existe</response>
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
        /// Criar novo Tema
        /// </summary>
        /// <param name="theme">NewThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Themes
        ///     {
        ///         "description" : "Biologia"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema criado</response>
        /// <response code="400">Erro na requisição</response>
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
        /// Atualizar Tema
        /// </summary>
        /// <param name="uptheme">NewThemeDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Themes
        ///     {
        ///         "description" : "Matematica"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna tema atualizado</response>
        /// <response code="400">Erro na requisição</response>
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
        /// Deletar tema pelo Id
        /// </summary>
        /// <param name="delTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Tema deletado</response>
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
