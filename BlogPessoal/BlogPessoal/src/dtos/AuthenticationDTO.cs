using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Abstract: Mirror class to create a new Authentication</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 13/05/2022 | Horario 11:55</para>
    /// </summary>
    public class AuthenticationDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
    /// <summary>
    /// <para>Abstract: Mirror class to update a Authentication</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022 | Horario 11:55</para>
    /// </summary>
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, TypeUser type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
