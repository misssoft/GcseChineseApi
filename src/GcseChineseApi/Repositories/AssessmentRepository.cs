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
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly GcseChineseDbContext _context;
        private readonly ILogger<AssessmentRepository> _logger;

        public AssessmentRepository(GcseChineseDbContext context, ILogger<AssessmentRepository> logger )
        {
            _context = context;
            _logger = logger;
        }

        public void AddAssessment(Assessment newAssessment)
        {
            try
            {
                _context.Add(newAssessment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
            }

        }

        public void AddExampaper(String assessmentCode, Exampaper newPaper)
        {
            var theAssessment = GetAssessmentByCode(assessmentCode);
            try
            {
                theAssessment?.Exampapers.Add(newPaper);
                _context.Exampapers.Add(newPaper);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot insert paper", ex);
            } 
        }

       
        public IEnumerable<Assessment> GetAllAssessments()
        {
            try
            {
                return _context.Assessments.ToList().OrderBy(a => a.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
            
        }

        public IEnumerable<Assessment> GetAllAssessmentswithExampapers()
        {
            try
            {
                return _context.Assessments.Include(a => a.Exampapers).ToList().OrderBy(a => a.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get data", ex);
                return null;
            }
            

        }

        public Assessment GetAssessmentByCode(string code)
        {
            try
            {
                return _context.Assessments.Include(a => a.Exampapers).FirstOrDefault(a => a.Code == code);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get Assessment", ex);
                return null;
            }
            
        }

        public IEnumerable<Exampaper> GetPapersByCode(string code)
        {
            try
            {
                var assessment = GetAssessmentByCode(code);
                return assessment?.Exampapers?.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot get Assessment's Papers", ex);
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
                _logger.LogError("Cannot save Assessments", ex);
                return false;
            }
        }

        public void UpdateAssessment(Assessment updatedAssessment)
        {
            try
            {
                _context.Assessments.Update(updatedAssessment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot update Assessment", ex);
            }
        }


        public void DeleteAssessment(Assessment assessment)
        {
            try
            {
                _context.Assessments.Remove(assessment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot update Assessment", ex);
            }
        }

        
    }
}
