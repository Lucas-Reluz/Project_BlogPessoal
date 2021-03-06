using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Abstract: Mirror class to create a new post</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
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
    /// <para>Abstract Mirror class to update a post</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 12:29</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required]
        public int Id { get; set; }
        
        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(int id,string title, string description, string photo, string descriptionTheme)
        {
            Id = id;
            Title = title;
            Description = description;
            Photo = photo;
            DescriptionTheme = descriptionTheme;
        }
    }
}
