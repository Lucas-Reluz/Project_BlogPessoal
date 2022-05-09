using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("id/{idPost}")]
        public IActionResult GetPostsById([FromRoute] int postId)
        {
            var post = _repository.GetPostById(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("search")]
        public IActionResult GetPostsBySearch([FromQuery] string title, [FromQuery] string descriptionTheme, [FromQuery] string creatorName)
        {
            var posts = _repository.GetPostsbySearch(title, descriptionTheme, creatorName);
            
            if(posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        [HttpPost]
        public IActionResult NewPost([FromBody] NewPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewPost(post);
            return Created($"api/Posts", post);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO upPost)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            _repository.UpdatePost(upPost);
            return Ok(upPost);
        }

        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();        
        }
    }
    #endregion
}
