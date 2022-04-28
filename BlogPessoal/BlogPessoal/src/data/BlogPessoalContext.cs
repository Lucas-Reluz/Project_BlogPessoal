using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
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
