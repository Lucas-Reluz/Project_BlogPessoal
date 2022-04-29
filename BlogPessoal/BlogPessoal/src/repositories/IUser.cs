using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de usuario</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 10:21</para>
    /// </summary>
    public interface IUser
    {
        void NewUser(NewUserDTO user);
        void UpdateUser(UpdateUserDTO user);
        void DeleteUser(int id);

        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByUsername(string name);
    }
}
