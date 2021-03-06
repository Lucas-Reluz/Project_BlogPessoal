using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    /// <summary>
    /// <para>Abstract: Class responsible for implementing ITheme</para>
    /// <para>Created by: Lucas Reluz</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 12/05/2022 | 12:11</para>
    /// </summary>
    public class ThemeRepository : ITheme
    {
        #region Atributos
        
        private readonly BlogPessoalContext _context;

        #endregion

        #region Construtores
        
        public ThemeRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion

        #region Metodos
        /// <summary>
        /// <para>Resumo:Asynchronous method responsible for delete a theme</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="id">Id Tema</param>
        /// <para>Data: 12/05/2022 | 12:11</para>
        /// </summary>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting all themes</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <para>Data: 12/05/2022 | 12:12</para>
        /// </summary>
        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting theme by description</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="description">Descricao tema</param>
        /// <para>Data: 12/05/2022 | 12:13</para>
        /// </summary>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes.Where(t => t.Description.Contains(description)).ToListAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for getting theme by Id</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="id">Id Tema</param>
        /// <para>Data: 12/05/2022 | 12:14</para>
        /// </summary>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for creating new theme</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="theme">NewThemeDTO</param>
        /// <para>Data: 12/05/2022 | 12:14</para>
        /// </summary>
        public async Task NewThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description,
            });
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// <para>Resumo: Asynchronous method responsible for updated theme</para>
        /// <para>Created by: Lucas Reluz</para>
        /// <para>Version: 1.0</para>
        /// <param name="theme">UpdateThemeDTO</param>
        /// <para>Data: 12/05/2022 | 12:15</para>
        /// </summary>
        public async Task UpdateThemeAsync(UpdateThemeDTO theme)
        {
            var _theme = await GetThemeByIdAsync(theme.Id);
            _theme.Description = theme.Description;
            _context.Themes.Update(_theme);
        }
    }
}
#endregion Metodos