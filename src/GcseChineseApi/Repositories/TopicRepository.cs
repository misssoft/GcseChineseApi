using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;
using GcseChineseApi.Models;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Repositories
{
    public class TopicRepository: ITopicRepository
    {
        private readonly GcseChineseDbContext _context;
        private readonly ILogger<TopicRepository> _logger;

        public TopicRepository(GcseChineseDbContext context, ILogger<TopicRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Topic> GetAllTopics()
        {
            try
            {
                return _context.Topics.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }

       
        public Topic GetTopicById(int id)
        {
            try
            {
                return _context.Topics.FirstOrDefault(t => t.TopicId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }

        public Topic GetTopicByName(string name)
        {
            try
            {
                return _context.Topics.FirstOrDefault(t => t.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
        }
    }
}
