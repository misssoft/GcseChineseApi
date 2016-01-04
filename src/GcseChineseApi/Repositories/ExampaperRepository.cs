using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;
using GcseChineseApi.Models;
using Microsoft.Extensions.Logging;

namespace GcseChineseApi.Repositories
{
    public class ExampaperRepository : IExampaperRepository
    {

        private readonly GcseChineseDbContext _context;
        private readonly ILogger<AssessmentRepository> _logger;

        public ExampaperRepository(GcseChineseDbContext context, ILogger<AssessmentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Exampaper> GetAllPapers()
        {
            return _context.Exampapers.ToList().OrderByDescending(p=>p.Year);
        }

        public IEnumerable<Exampaper> GetPaperByCode(string code)
        {
            return _context.Exampapers.Where(p => p.Code == code).ToList().OrderByDescending(p => p.Year);
        }

        public IEnumerable<Exampaper> GetPaperByYear(int year)
        {
            return _context.Exampapers.Where(p => p.Year == year).ToList().OrderBy(p => p.Code);
        }

        public IEnumerable<Exampaper> GetPaperByYearAndCode(int year, string code)
        {
            return _context.Exampapers.Where(p => p.Year == year  & p.Code == code).ToList().OrderBy(p => p.Code);
        }

        public void Delete(Exampaper deletePaper)
        {
            try
            {
                _context.Exampapers.Remove(deletePaper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot delete Example Papers", ex);
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
                _logger.LogError("Cannot save Exam Papers", ex);
                return false;
            }
        }

        public Exampaper GetPaperById(int id)
        {
            return _context.Exampapers.FirstOrDefault(p => p.ExampaperId == id);
        }
    }
}
