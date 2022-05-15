using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_posts no banco.</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022 | 11:52</para>
    /// </summary>
    #region Tabela de Postagens
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength (100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [ForeignKey("fk_user")]
        public UserModel Creator { get; set; }

        [ForeignKey("fk_theme")]
        public ThemeModel SelectTheme { get; set; }
    }
}
#endregion