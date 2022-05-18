using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Atributos

        private readonly IPost _repository;

        #endregion

        #region Construtores

        public PostController(IPost repository)
        {
            _repository = repository;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Get post by Id
        /// </summary>
        /// <param name="postId">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return post</response>
        /// <response code="404">Post does not exist</response>
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostsByIdAsync([FromRoute] int postId)
        {
            var post = await _repository.GetPostByIdAsync(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Post list</response>
        /// <response code="204">Empty list</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Get posts by search
        /// </summary>
        /// <param name="title">string</param>
        /// <param name="descriptionTheme">string</param>
        /// <param name="creatorName">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return posts</response>
        /// <response code="204">Posts do not exist for this search</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetPostsBySearchAsync([FromQuery] string title, [FromQuery] string descriptionTheme, [FromQuery] string creatorName)
        {
            var posts = await _repository.GetPostsbySearchAsync(title, descriptionTheme, creatorName);
            
            if(posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        /// <summary>
        /// Create new post
        /// </summary>
        /// <param name="post">NovaPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Posts
        ///     {  
        ///        "titulo": "Vendo Fiat Uno", 
        ///        "descricao": "Aceito 7k na mão ou 8k Parcelando,
        ///        "foto": "URLFOTODOFIATUNO",
        ///        "emailCriador": "cleiton@email.com",
        ///        "descricaoTema": "CARROS"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created post</response>
        /// <response code="400">requisition error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task <ActionResult> NewPostAsync([FromBody] NewPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(post);
            return Created($"api/Posts", post);
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="upPost">AtualizarPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Posts
        ///     {
        ///        "id": 1,   
        ///        "title": "Já vendi o Fiat Uno", 
        ///        "description": "Mas ainda tenho outras coisas pra vender, quem quiser DM",
        ///        "photo": "URLDAIMAGEMDASCOISAS",
        ///        "descriptionTheme": "ELETRONICOS"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return deleted post</response>
        /// <response code="400">requisition error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync([FromBody] UpdatePostDTO upPost)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            await _repository.UpdatePostAsync(upPost);
            return Ok(upPost);
        }

        /// <summary>
        /// Delete post by Id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Deleted Post</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();        
        }
    }
    #endregion
}
