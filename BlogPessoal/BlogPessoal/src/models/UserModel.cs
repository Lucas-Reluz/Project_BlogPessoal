using BlogPessoal.src.utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_users no banco.</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022 | 11:52</para>
    /// </summary>
    #region Tabela de Usuarios
    [Table("tb_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        
        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(50)]
        public string Password { get; set; }

        public string Photo { get; set; }

        [Required]
        public TypeUser Type { get; set; }

        [JsonIgnore]
        public List<PostModel> MyPosts { get; set; }
    }
}
#endregion