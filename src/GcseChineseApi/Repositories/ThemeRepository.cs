using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;
using GcseChineseApi.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Repositories
{
    public class ThemeRepository : IThemeRepository
    {

        private readonly GcseChineseDbContext _context;
        private readonly ILogger<AssessmentRepository> _logger;

        public ThemeRepository(GcseChineseDbContext context, ILogger<AssessmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddTheme(Theme newTheme)
        {
            try
            {
                _context.Add(newTheme);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot insert Theme", ex);
            }
        }

        public void AddTopic(int id, Topic newTopic)
        {
            try
            {
                var currentTheme = GetThemeById(id);
                currentTheme?.Topics.Add(newTopic);
                _context.Topics.Add(newTopic);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot insert Topic", ex);
            }
        }

        public IEnumerable<Theme> GetAllThemes()
        {
            try
            {
                return _context.Themes.ToList().OrderBy(a => a.ThemeId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }

        public IEnumerable<Theme> GetAllThemeswithTopics()
        {
            try
            {
               return _context.Themes.Include(a => a.Topics).ToList().OrderBy(a => a.ThemeId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }

        public Theme GetThemeById(int id)
        {
            try
            {
                return _context.Themes.Include(t=>t.Topics).FirstOrDefault(t => t.ThemeId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }

        }

        public IEnumerable<Topic> GetTopicsbyThemeId(int id)
        {
            try
            {
                var theme = GetThemeById(id);
                return theme?.Topics?.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }

        public bool SaveAll()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot save data", ex);
                return false;
            }
        }
    }
}
