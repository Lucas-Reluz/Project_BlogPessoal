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
    public class UserRepositoryTest
    {
        private BlogPessoalContext _context;
        private IUser _repository;

        [TestMethod]
        public void CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            _repository.NewUser(
                new NewUserDTO(
                    "Lucas Reluz",
                    "lucas@email.com",
                    "134652",
                    "URLFOTO")
            );

            _repository.NewUser(
                new NewUserDTO(
                    "Juninho Augusto",
                    "junin@email.com",
                    "134652",
                    "URLFOTO")
            );

            _repository.NewUser(
                new NewUserDTO(
                    "Jose Mariano",
                    "jose@email.com",
                    "134652",
                    "URLFOTO")
            );

            _repository.NewUser(
                new NewUserDTO(
                    "Laurino Marcelo",
                    "laurino@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }

        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.NewUser(
                new NewUserDTO(
                    "Augusto Limeira",
                    "augusto@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmail("augusto@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.NewUser(
                new NewUserDTO(
                    "Neusa Boaz",
                    "neusa@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando pesquiso pelo id 1
            var user = _repository.GetUserById(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            _repository.NewUser(
                new NewUserDTO(
                    "Estefânia Boaz",
                    "estefania@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando atualizamos o usuario
            var old = _repository.GetUserByEmail("estefania@email.com");
            _repository.UpdateUser(
                new UpdateUserDTO(
                    1,
                    "Estefânia Moura",
                    "123456",
                    "URLFOTONOVA")
            );

            //THEN - Então, pesquisa deve retornar nome Estefânia Moura
            Assert.AreEqual(
                "Estefânia Moura",
                _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
                "123456",
                _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
        }
    }
}