using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Classe responsavel por representar tb_themes no banco.</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
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