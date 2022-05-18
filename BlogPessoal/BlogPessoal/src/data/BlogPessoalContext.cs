using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    /// <summary>
    /// <para>Abstract: Class context, responsible for carry contexts and define dbsets</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 13/05/2022 | Horario 11:55</para>
    /// </summary>
    public class BlogPessoalContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ThemeModel> Themes { get; set; }        
        public DbSet<PostModel> Posts { get; set; }
    
        public BlogPessoalContext(DbContextOptions<BlogPessoalContext> opt) : base(opt)
        {

        }    
    }
}
