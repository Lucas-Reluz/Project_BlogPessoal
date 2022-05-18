using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Abstract: Responsible for representing User CRUD actions</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 10:21</para>
    /// </summary>
    public interface IUser
    {
        Task NewUserAsync(NewUserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(int id);

        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmailAsync(string email);
        Task<List<UserModel>> GetUserByUsernameAsync(string name);
    }
}
