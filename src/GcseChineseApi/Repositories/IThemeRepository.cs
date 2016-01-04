using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.Repositories
{
    public interface IThemeRepository
    {

        IEnumerable<Theme> GetAllThemes();
        IEnumerable<Theme> GetAllThemeswithTopics();
        void AddTheme(Theme newTheme);
        bool SaveAll();
        Theme GetThemeById(int id);
        void AddTopic(int id, Topic newTopic);

        IEnumerable<Topic> GetTopicsbyThemeId(int id);
    }
}
