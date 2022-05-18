using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Abstract: Responsible for representing Theme CRUD actions</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 29/04/2022 / Horario 11:26</para>
    /// </summary>
    public interface ITheme
    {
        Task NewThemeAsync(NewThemeDTO theme);
        Task UpdateThemeAsync(UpdateThemeDTO theme);
        Task DeleteThemeAsync(int id);
        Task<ThemeModel> GetThemeByIdAsync(int id);
        Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description);
        Task<List<ThemeModel>> GetAllThemesAsync();
    }
}
