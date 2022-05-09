using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
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
        public void CreateThreePostsInSystemReturnThree()
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
            _repositoryU.NewUser(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            _repositoryU.NewUser(
                new NewUserDTO
                ("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 2 temas
            _repositoryT.NewTheme(new NewThemeDTO("C#"));
            _repositoryT.NewTheme(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco todas as postagens
            // THEN - Eu tenho 3 postagens
            Assert.AreEqual(3, _repositoryP.GetAllPosts().Count());
        }

        [TestMethod]
        public void UpdatePostReturnPostActualized()
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
            _repositoryU.NewUser(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 1 tema
            _repositoryT.NewTheme(new NewThemeDTO("COBOL"));
            _repositoryT.NewTheme(new NewThemeDTO("C#"));

            // AND - E que registro 1 postagem
            _repositoryP.NewPost(
                new NewPostDTO(
                    "COBOL é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "COBOL"
                )
            );

            // WHEN - Quando atualizo postagem de id 1
            _repositoryP.UpdatePost(
                new UpdatePostDTO(
                    1,
                    "C# é muito massa",
                    "C# é muito utilizada no mundo",
                    "URLDAFOTOATUALIZADA",
                    "C#"
                )
            );

            // THEN - Eu tenho a postagem atualizada
            Assert.AreEqual(
                "C# é muito massa",
                _repositoryP.GetPostById(1).Title
            );
            Assert.AreEqual(
                "C# é muito utilizada no mundo",
                _repositoryP.GetPostById(1).Description
            );
            Assert.AreEqual(
                "URLDAFOTOATUALIZADA",
                _repositoryP.GetPostById(1).Photo
            );
            Assert.AreEqual(
                "C#",
                _repositoryP.GetPostById(1).SelectTheme.Description
            );
        }

        [TestMethod]
        public void GetPostBySearchReturnCustom()
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
            _repositoryU.NewUser(
                new NewUserDTO
                ("Gustavo Boaz", "gustavo@email.com", "134652", "URLDAFOTO")
            );

            _repositoryU.NewUser(
                new NewUserDTO
                ("Catarina Boaz", "catarina@email.com", "134652", "URLDAFOTO")
            );

            // AND - E que registro 2 temas
            _repositoryT.NewTheme(new NewThemeDTO("C#"));
            _repositoryT.NewTheme(new NewThemeDTO("Java"));

            // WHEN - Quando registro 3 postagens
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# é muito massa",
                    "É uma linguagem muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "C# pode ser usado com Testes",
                    "O teste unitário é importante para o desenvolvimento",
                    "URLDAFOTO",
                    "catarina@email.com",
                    "C#"
                )
            );
            _repositoryP.NewPost(
                new NewPostDTO(
                    "Java é muito massa",
                    "Java também é muito utilizada no mundo",
                    "URLDAFOTO",
                    "gustavo@email.com",
                    "Java"
                )
            );

            // WHEN - Quando eu busco as postagen
            // THEN - Eu tenho as postagens que correspondem aos criterios
            Assert.AreEqual(
                2,
                _repositoryP.GetPostsbySearch("massa", null, null).Count
            );
            Assert.AreEqual(
                2,
                _repositoryP.GetPostsbySearch(null, "C#", null).Count
            );
            Assert.AreEqual(
                2,
                _repositoryP.GetPostsbySearch(null, null, "Gu").Count
            );
        }

    }
}