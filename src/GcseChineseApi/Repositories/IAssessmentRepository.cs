using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.Repositories
{
    public interface IAssessmentRepository
    {
        IEnumerable<Assessment> GetAllAssessments();
        IEnumerable<Assessment> GetAllAssessmentswithExampapers();
        void AddAssessment(Assessment newAssessment);
        void UpdateAssessment(Assessment updatedAssessment);
        void DeleteAssessment(Assessment assessment);
        bool SaveAll();
        Assessment GetAssessmentByCode(string code);
        IEnumerable<Exampaper> GetPapersByCode(string code); 
        void AddExampaper(string code, Exampaper newPaper);
     }
}
