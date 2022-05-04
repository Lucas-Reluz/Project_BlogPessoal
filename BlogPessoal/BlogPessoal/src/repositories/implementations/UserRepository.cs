using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class UserRepository : IUser
    {
        #region Atributos
        
        private readonly BlogPessoalContext _context;

        #endregion Atributos

        #region Construtores
        
        public UserRepository(BlogPessoalContext context)
        {
          _context = context;
        }
        
        #endregion Construtores

        #region Metodos
        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
            
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public List<UserModel> GetUserByUsername(string name)
        {
            return _context.Users.Where(u => u.Name.Contains(name)).ToList();
        }

        public void NewUser(NewUserDTO user)
        {
            _context.Users.Add(new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,                
            });
            _context.SaveChanges();
        }

        public void UpdateUser(UpdateUserDTO user)
        {
            var _user = GetUserById(user.Id);
            _user.Name = user.Name;
            _user.Password = user.Password;
            _user.Photo = user.Photo;
            _context.Update(_user);
            _context.SaveChanges();
        }
    }
}
#endregion Metodos