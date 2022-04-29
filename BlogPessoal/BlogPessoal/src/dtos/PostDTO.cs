using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar uma nova postagem</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 12:00</para>
    /// </summary>
    public class NewPostDTO
    {
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required, StringLength(30)]
        public string CreatorEmail { get; set; }

        public string DescriptionTheme { get; set; }

        public NewPostDTO(string title, string description, string photo, string creatorEmail, string descriptiontheme)
        {
            Title = title;
            Description = description;
            Photo = photo;
            CreatorEmail = creatorEmail;
            DescriptionTheme = descriptiontheme;
        }
    }
    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar uma nova postagem</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 12:29</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(string title, string description, string photo, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Photo = photo;
            DescriptionTheme = descriptionTheme;
        }
    }
}
