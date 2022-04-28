using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPessoal.src.models;
using System.Linq;

namespace BlogPessoalTeste.Tests.data
{
    [TestClass]
    public class BlogPessoalContextTest
    {
        private BlogPessoalContext _context;
        
        [TestInitialize]
        public void setup()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>().UseInMemoryDatabase(databaseName: "db_blogpessoal").Options;
            _context = new BlogPessoalContext(opt);
        }

        [TestMethod]
        public void InsertNewUserInDatabaseReturnUser()
        {
            UserModel user = new UserModel();

            user.Name = "Karol Boaz";
            user.Email = "karol@email.com";
            user.Password = "134566";
            user.Photo = "FOTAUMMENO";

            _context.Users.Add(user); // Adicionando usuario
            
            _context.SaveChanges(); // Commita criação

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Photo == "FOTAUMMENO"));

            //Assert.AreEqual(1, 1);
        }
    }
}
