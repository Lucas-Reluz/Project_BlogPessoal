using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Abstract: Responsible for representing Post CRUD actions</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 11:51</para>
    /// </summary>
    public interface IPost
    {
        Task NewPostAsync(NewPostDTO post);
        Task UpdatePostAsync(UpdatePostDTO post);
        Task DeletePostAsync(int id);
        Task<PostModel> GetPostByIdAsync(int id);
        Task<List<PostModel>> GetPostsbySearchAsync(string title, string descriptionTheme, string creatorName);
        Task<List<PostModel>> GetAllPostsAsync();
    }
}
