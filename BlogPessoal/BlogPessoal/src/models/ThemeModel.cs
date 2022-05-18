using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Abstract: Class responsible for representing tb_themes in the database</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022 | 11:52</para>
    /// </summary>
    #region Tabela de Temas
    [Table("tb_themes")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }
    
        [JsonIgnore]
        public List<PostModel> RelatedPosts { get; set; }
    }
}
#endregion