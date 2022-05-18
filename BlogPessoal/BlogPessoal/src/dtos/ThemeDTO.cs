using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Abstract: Mirror class to create a new theme</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 11:45</para>
    /// </summary>
    public class NewThemeDTO
    {
        [Required, StringLength(20)]
        public string Description { get; set; }

        public NewThemeDTO(string description)
        {
            Description = description;
        }
    }
    /// <summary>
    /// <para>Abstract: Mirror class to update a theme</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 11:45</para>
    /// </summary>
    public class UpdateThemeDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }

        public UpdateThemeDTO(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
