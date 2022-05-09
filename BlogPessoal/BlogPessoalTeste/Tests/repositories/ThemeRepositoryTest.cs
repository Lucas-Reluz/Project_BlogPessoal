using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoal.src.test.repositories
{
   
    [TestClass]
    public class ThemeRepositoryTest
    {
        private BlogPessoalContext _context;
        private ITheme _repository;

        [TestMethod]
        public void CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 4 temas no banco
            _repository.NewTheme(new NewThemeDTO("C#"));
            _repository.NewTheme(new NewThemeDTO("Java"));
            _repository.NewTheme(new NewThemeDTO("Python"));
            _repository.NewTheme(new NewThemeDTO("JavaScript"));

            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, _repository.GetAllThemes().Count);
        }

        [TestMethod]
        public void PegarTemaPeloIdRetornaTema1()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro C# no banco
            _repository.NewTheme(new NewThemeDTO("C#"));

            //WHEN - Quando pesquiso pelo id 1
            var theme = _repository.GetThemeById(1);

            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", theme.Description);
        }

        [TestMethod]
        public void PegaTemaPelaDescricaoRetornadoisTemas()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Java no banco
            _repository.NewTheme(new NewThemeDTO("Java"));
            //AND - E que registro JavaScript no banco
            _repository.NewTheme(new NewThemeDTO("JavaScript"));

            //WHEN - Quando que pesquiso pela descricao Java
            var themes = _repository.GetThemeByDescription("Java");

            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, themes.Count);
        }

        [TestMethod]
        public void AlterarTemaPythonRetornaTemaCobol()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro Python no banco
            _repository.NewTheme(new NewThemeDTO("Python"));

            //WHEN - Quando passo o Id 1 e a descricao COBOL
            _repository.UpdateTheme(new UpdateThemeDTO(1, "COBOL"));

            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL", _repository.GetThemeById(1).Description);
        }

        [TestMethod]
        public void DeletarTemasRetornaNulo()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal5")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            //GIVEN - Dado que registro 1 temas no banco
            _repository.NewTheme(new NewThemeDTO("C#"));

            //WHEN - quando deleto o Id 1
            _repository.DeleteTheme(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(_repository.GetThemeById(1));
        }
    }
}