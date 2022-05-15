using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por implementar IPost</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022 | 12:17</para>
    /// </summary>
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
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por deletar uma Postagem</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <param name="id">Id Postagem</param>
        /// <para>Data: 12/05/2022 | 12:20</para>
        /// </summary>
        public async Task DeletePostAsync(int id)
        {
            _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por pegar todas as postagens</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <para>Data: 12/05/2022 | 12:21</para>
        /// </summary>
        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por pegar uma postagem pelo Id</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <param name="id">Id Postagem</param>
        /// <para>Data: 12/05/2022 | 12:21</para>
        /// </summary>
        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por deletar uma Postagem</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <param name="title">Titulo</param>
        /// <param name="descriptionTheme">Descricao Tema</param>
        /// <param name="creatorName">Nome do Criador</param>
        /// <para>Data: 12/05/2022 | 12:23</para>
        /// </summary>
        public async Task<List<PostModel>> GetPostsbySearchAsync(string title, string descriptionTheme, string creatorName)
        {
            switch (title, descriptionTheme, creatorName)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

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
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por criar nova postagem</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <param name="post">NewPostDTO</param>
        /// <para>Data: 12/05/2022 | 12:24</para>
        /// </summary>
        public async Task NewPostAsync(NewPostDTO post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(u => u.Email == post.CreatorEmail),
                SelectTheme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme)
            });
            await _context.SaveChangesAsync();

        }
        /// <summary>
        /// <para>Resumo: Metodo assincrono responsavel por atualizar uma postagem</para>
        /// <para>Criado por: Lucas Reluz</para>
        /// <para>Versão: 1.0</para>
        /// <param name="post">UpdatePostDTO</param>
        /// <para>Data: 12/05/2022 | 12:24</para>
        /// </summary>
        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            var _post = await GetPostByIdAsync(post.Id);
            _post.Title = post.Title;
            _post.Description = post.Description;
            _post.Photo = post.Photo;
            _post.SelectTheme = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);
            _context.Update(_post);
            await _context.SaveChangesAsync();
        }
    }
}
#endregion