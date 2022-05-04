using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class PostRepository : IPost
    {
        #region Atributos
        private readonly BlogPessoalContext _context;
        #endregion

        #region Construtores

        public PostRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos
        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<PostModel> GetPostsbySearch(string title, string descriptionTheme, string creatorName)
        {
            switch (title, descriptionTheme, creatorName)
            {
                case (null, null, null):
                    return GetAllPosts();
                
                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(creatorName))
                    .ToList();
                
                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p => p.SelectTheme.Description.Contains(descriptionTheme))
                    .ToList();
                
                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();
                
                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.SelectTheme.Description.Contains(descriptionTheme))
                    .ToList();
                
                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.SelectTheme.Description.Contains(descriptionTheme) &
                    p.Creator.Name.Contains(creatorName))
                    .ToList();
                
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(creatorName))
                    .ToList();
                
                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.SelectTheme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.SelectTheme.Description.Contains(descriptionTheme) |
                    p.Creator.Name.Contains(creatorName))
                    .ToList();

            }
        }

            public void NewPost(NewPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.CreatorEmail),
                SelectTheme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme)
            });
            _context.SaveChanges();

        }

        public void UpdatePost(UpdatePostDTO post)
        {
            var _post = GetPostById(post.Id);
            _post.Title = post.Title;
            _post.Description = post.Description;
            _post.Photo = post.Photo;
            _post.SelectTheme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);
            _context.Update(_post);
            _context.SaveChanges();
        }
    }
}
#endregion