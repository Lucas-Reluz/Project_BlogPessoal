using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Tests.repositories
{
    [TestClass]
    public class PostRepositoryTest
    {

        private BlogPessoalContext _context;
        private IUser _repositoryU;
        private ITheme _repositoryT;
        private IPost _repositoryP;

        [TestMethod]
        public async Task CreateThreePostsInSystemReturnThree()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                 .UseInMemoryDatabase(databaseName: "db_blogpessoal21")
                 .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                (TypeUser.USER, "Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            await _repositoryU.NewUserAsync(
                new NewUserDTO
                (TypeUser.USER, "Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            var posts = await _repositoryP.GetAllPostsAsync();
            // WHEN - Quando eu busco todas as postagens
            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, posts.Count());
        }

        [TestMethod]
        public async Task UpdatePostReturnPostActualized()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 1 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                (TypeUser.USER, "Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 1 tema
            await _repositoryT.NewThemeAsync(new NewThemeDTO("COBOL"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            await _repositoryP.UpdatePostAsync(
                new UpdatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            var posts = await _repositoryP.GetPostByIdAsync(1);
            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual(
                "C# é muito massa",
               posts.Title
            );
            Assert.AreEqual(
                "C# é muito utilizada no mundo",
                posts.Description
            );
            Assert.AreEqual(
                "URLDAFOTOATUALIZADA",
                posts.Photo
            );
            Assert.AreEqual(
                "C#",
                posts.SelectTheme.Description
            );
        }

        [TestMethod]
        public async Task GetPostBySearchReturnCustom()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // GIVEN - Dado que registro 2 usuarios
            await _repositoryU.NewUserAsync(
                new NewUserDTO
                (TypeUser.USER, "Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            await _repositoryU.NewUserAsync(
                new NewUserDTO
                (TypeUser.USER, "Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO")
            ); ;

            // AND - E que registro 2 temas
            await _repositoryT.NewThemeAsync(new NewThemeDTO("C#"));
            await _repositoryT.NewThemeAsync(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            await _repositoryP.NewPostAsync(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            var posts1 = await _repositoryP.GetPostsbySearchAsync("massa", null, null);
            var posts2 = await _repositoryP.GetPostsbySearchAsync(null, "C#", null);
            var posts3 = await _repositoryP.GetPostsbySearchAsync(null, null, "Gu");
            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2,
                 posts1.Count
            );
            Assert.AreEqual(
                2,
                 posts2.Count
            );
            Assert.AreEqual(
                2,
                 posts3.Count
            );
        }

    }
}