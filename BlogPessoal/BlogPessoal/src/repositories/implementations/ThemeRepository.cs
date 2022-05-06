using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
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
        public void DeleteTheme(int id)
        {
            _context.Themes.Remove(GetThemeById(id));
            _context.SaveChanges();
        }

        public List<ThemeModel> GetAllThemes()
        {
            return _context.Themes.ToList();
        }

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes.Where(t => t.Description.Contains(description)).ToList();
        }

        public ThemeModel GetThemeById(int id)
        {
            return _context.Themes.FirstOrDefault(t => t.Id == id);
        }

        public void NewTheme(NewThemeDTO theme)
        {
            _context.Themes.Add(new ThemeModel
            {
                Description = theme.Description,
            });
            _context.SaveChanges();
        }

        public void UpdateTheme(UpdateThemeDTO theme)
        {
            var _theme = GetThemeById(theme.Id);
            _theme.Description = theme.Description;
            _context.Themes.Update(_theme);
        }
    }
}
#endregion Metodos