using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    /// <summary>
    /// <para>Resumo: Responsavel por representar ações de CRUD de Tema</para>
    /// <para>Criado por: Lucas Reluz</para>
    /// <para>Versão: 1.0</para>
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
