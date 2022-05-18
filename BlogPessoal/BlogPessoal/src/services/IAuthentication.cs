using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.services
{
    /// <summary>
    /// <para>Abstract: Responsible for representing Authentication CRUD actions</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 15/05/2022 Horario 15:43 </para>
    /// </summary>
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserWithoutDuplicatingAsync(NewUserDTO dto);
        string GenToken(UserModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto);

    }
}
