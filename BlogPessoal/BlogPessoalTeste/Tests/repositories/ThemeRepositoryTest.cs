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
        
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);
        }
        [TestMethod]
        public void CriarQuatroTemasNoBancoRetornaQuatroTemas2()
        {
            //GIVEN - Dado que registro 4 temas no banco
            _repository.NewTheme(new NewThemeDTO("C#"));
            _repository.NewTheme(new NewThemeDTO("Java"));
            _repository.NewTheme(new NewThemeDTO("Python"));
            _repository.NewTheme(new NewThemeDTO("JavaScript"));
            //THEN - Entao deve retornar 4 temas
            Assert.AreEqual(4, _repository.GetAllThemes().Count);
        }
        [TestMethod]
        [DataRow(1)]
        public void PegarTemaPeloIdRetornaTema1(int id)
        {
            //GIVEN - Dado que pesquiso pelo id 1
            var theme = _repository.GetThemeById(id);
            //THEN - Entao deve retornar 1 tema
            Assert.AreEqual("C#", theme.Description);
        }
        [TestMethod]
        [DataRow("Java")]
        public void PegaTemaPelaDescricaoRetornadoisTemas(string description)
        {
            //GIVEN - Dado que pesquiso pela descricao Java
            var temas = _repository.GetThemeByDescription(description);
            //THEN - Entao deve retornar 2 temas
            Assert.AreEqual(2, temas.Count);
        }
        [TestMethod]
        [DataRow(3)]
        public void AlterarTemaPythonRetornaTemaCobol(int id)
        {
            //GIVEN - Dado que passo o Id 3 e o tema COBOL
            _repository.UpdateTheme(new UpdateThemeDTO(id, "COBOL"));
            //THEN - Entao deve retornar o tema COBOL
            Assert.AreEqual("COBOL",
            _repository.GetThemeById(id).Description);
        }
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(4)]
        public void DeletarTemasRetornaNulo(int id)
        {
            //GIVEN - Dado que passo o Id 1, 2, 3, 4
            _repository.DeleteTheme(id);
            //THEN - Entao deve retornar nulo
            Assert.IsNull(_repository.GetThemeById(id));
        }
    }
}