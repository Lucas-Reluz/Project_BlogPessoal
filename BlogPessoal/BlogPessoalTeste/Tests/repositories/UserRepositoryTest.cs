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
    //[TestClass]
    public class UserRepositoryTest : IDisposable
    {
        private BlogPessoalContext _context;
        private IUser _repositorie;
        
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _context = new BlogPessoalContext(opt);
            _repositorie = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            _repositorie.NewUser(
            new NewUserDTO(
            "Lucas Boaz",
            "lucas@email.com",
            "134652",
            "URLFOTO"));

            _repositorie.NewUser(
            new NewUserDTO(
            "Mallu Boaz",
            "mallu@email.com",
            "134652",
            "URLFOTO"));

            _repositorie.NewUser(
            new NewUserDTO(
            "Catarina Boaz",
            "catarina@email.com",
            "134652",
            "URLFOTO"));

            _repositorie.NewUser(
            new NewUserDTO(
            "Pamela Boaz",
            "pamela@email.com",
            "134652",
            "URLFOTO"));
        }
        
        [TestMethod]
        public void PesquisaUsuariosNoBancoRetornaQuatroUsuarios()
        {

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }
        
        [TestMethod]
        public void PesquisarUsuarioPorEmailRetornaNaoNulo()
        {
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repositorie.GetUserByEmail("catarina@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
        
        [TestMethod]
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuarioPamelaBoaz()
        {
            //WHEN - Quando pesquiso pelo id 6
            var user = _repositorie.GetUserById(4);
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Pamela Boaz", user.Name);
        }

        [TestMethod]
        public void DAtualizarUsuarioRetornaUsuarioAtualizado4()
        {
            //WHEN - Quando atualizamos o usuario
            _repositorie.UpdateUser(
                new UpdateUserDTO(
                    _context.Users.FirstOrDefault(u => u.Email == "lucas@email.com").Id,
                    "Lucas Alterado",
                    "123456",
                    "URLFOTONOVA"));

            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Name == "Lucas Alterado"));

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Password == "123456"));

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Photo == "URLFOTONOVA"));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
