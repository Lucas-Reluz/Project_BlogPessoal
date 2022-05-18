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
    public class UserRepositoryTest
    {
        private BlogPessoalContext _context;
        private IUser _repository;

        [TestMethod]
        public async Task CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal1")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro 4 usuarios no banco
            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Lucas Reluz",
                    "lucas@email.com",
                    "134652",
                    "URLFOTO")
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Juninho Augusto",
                    "junin@email.com",
                    "134652",
                    "URLFOTO")
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Jose Mariano",
                    "jose@email.com",
                    "134652",
                    "URLFOTO")
            );

            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Laurino Marcelo",
                    "laurino@email.com",
                    "134652",
                    "URLFOTO")
            ); ;

            //WHEN - Quando pesquiso lista total            
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }

        [TestMethod]
        public async Task PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal2")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Augusto Limeira",
                    "augusto@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmailAsync("augusto@email.com");

            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal3")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Neusa Boaz",
                    "neusa@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando pesquiso pelo id 1
            var user = await _repository.GetUserByIdAsync(1);

            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }

        [TestMethod]
        public async Task AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            // Definindo o contexto
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal4")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new UserRepository(_context);

            //GIVEN - Dado que registro um usuario no banco
            await _repository.NewUserAsync(
                new NewUserDTO(
                    TypeUser.USER,
                    "Estefânia Boaz",
                    "estefania@email.com",
                    "134652",
                    "URLFOTO")
            );

            //WHEN - Quando atualizamos o usuario
            var old = await _repository.GetUserByEmailAsync("estefania@email.com");
           await _repository.UpdateUserAsync(
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