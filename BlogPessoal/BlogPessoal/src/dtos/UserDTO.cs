using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar um novo usuario</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 10:47</para>
    /// </summary>
    public class NewUserDTO
    {        
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }
        
        public string Photo { get; set; }

        [Required]
        public TypeUser Type { get; set; }

        public NewUserDTO(TypeUser type, string name, string email, string password, string photo)
        {
            Type = type;
            Name = name;
            Email = email;
            Password = password;
            Photo = photo;
        }
    }
    
    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar um novo usuario</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 10:47</para>
    /// </summary>
    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        public string Photo { get; set; }
        
        public UpdateUserDTO(int id, string name, string password, string photo)
        {
            Id = id;
            Name = name;
            Password = password;
            Photo = photo;
        }
    }
}
