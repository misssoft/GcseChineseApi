using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.Repositories
{
    public interface IExampaperRepository
    {

        IEnumerable<Exampaper> GetAllPapers();
        IEnumerable<Exampaper> GetPaperByYear(int year);
        IEnumerable<Exampaper> GetPaperByCode(string code);
        IEnumerable<Exampaper> GetPaperByYearAndCode(int year, string code);
        void Delete(Exampaper updatePaper);
        bool SaveAll();
        Exampaper GetPaperById(int id);
    }
}
