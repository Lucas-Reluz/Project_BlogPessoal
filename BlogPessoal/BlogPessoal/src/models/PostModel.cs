using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
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