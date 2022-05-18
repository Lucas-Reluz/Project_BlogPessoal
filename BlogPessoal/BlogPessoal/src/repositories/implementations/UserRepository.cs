using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>Abstract: Class responsible for implementing IUser</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022 | 12:02</para>
    /// </summary>
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
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for deleting the User</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="id">Id Usuario</param>
        /// <para>Data: 12/05/2022 | 12:05</para>
        /// </summary>
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
            
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting user by email</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="email">Email Usuario</param>
        /// <para>Data: 12/05/2022 | 12:06</para>
        /// </summary>
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting user by Id</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="id">Id Usuario</param>
        /// <para>Data: 12/05/2022 | 12:07</para>
        /// </summary>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting user by name</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="name">Nome do Usuario</param>
        /// <para>Data: 12/05/2022 | 12:07</para>
        /// </summary>
        public async Task<List<UserModel>> GetUserByUsernameAsync(string name)
        {
            return await _context.Users.Where(u => u.Name.Contains(name)).ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for creating a new user</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="user">NewUserDTO</param>
        /// <para>Data: 12/05/2022 | 12:09</para>
        /// </summary>
        public async Task NewUserAsync(NewUserDTO user)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Type = user.Type,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,                
            });
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for updated a user</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="user">UpdateUserDTO</param>
        /// <para>Data: 12/05/2022 | 12:09</para>
        /// </summary>
        public async Task UpdateUserAsync(UpdateUserDTO user)
        {
            var _user = await GetUserByIdAsync(user.Id);
            _user.Name = user.Name;
            _user.Password = user.Password;
            _user.Photo = user.Photo;
            _context.Update(_user);
            await _context.SaveChangesAsync();
        }
    }
}
#endregion Metodos