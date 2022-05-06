using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

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
        void NewTheme(NewThemeDTO theme);
        void UpdateTheme(UpdateThemeDTO theme);
        void DeleteTheme(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetThemeByDescription(string description);
        List<ThemeModel> GetAllThemes();
    }
}
