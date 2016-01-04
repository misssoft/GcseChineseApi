using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.Repositories
{
    public interface ITopicRepository
    {
        IEnumerable<Topic> GetAllTopics();
        Topic GetTopicById(int id);
        Topic GetTopicByName (string name);
    }
}
