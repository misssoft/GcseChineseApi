using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.Models
{
    public class GcseSeededData
    {
        private readonly GcseChineseDbContext _context;

        public GcseSeededData(GcseChineseDbContext context)
        {
            _context = context;
        }

        public void EnsureSeedData()
        {
            Seed_Themes();
            Seed_Assessments();
        }

        private void Seed_Themes()
        {
            if (!_context.Themes.Any())
            {
                var theme = new Theme()
                {
                    Name = "Media & Culture",
                    IsActive = true,
                    Topics = new List<Topic>()
                    {
                        new Topic() {Name="Music" },
                        new Topic() {Name="Film" },
                        new Topic() {Name="Reading" },
                        new Topic() {Name="Fashion" },
                        new Topic() {Name="Celebrities" },
                        new Topic() {Name="Religion" },
                        new Topic() {Name="Blogs" },
                        new Topic()
                        {
                            Name="Internet",
                            Resources = new List<Resource>()
                            {
                                new Resource() { Uri = "www.google.com"},
                                new Resource() { Uri = "www.yahoo.com"},
                                new Resource() { Uri = "www.msn.com"}
                            }
                            
                        },
                    }

                };

                _context.Themes.Add(theme);

                if (theme.Topics != null)
                {
                    _context.Topics.AddRange(theme.Topics);
                    foreach (var topic in theme.Topics)
                    {
                        if (topic.Resources != null)
                        {
                            _context.Resources.AddRange(topic.Resources);
                        }
                     }

                }
                _context.SaveChanges();
            }
        }

        private void Seed_Assessments()
        {
            if (!_context.Assessments.Any())
            {
                var testAssessment = new Assessment()
                {
                    Code = "testA",
                    Description = "DescriptionA",
                    Level = "H",
                    Marks = 40,
                    Timing = "30 minutes",
                    Percentage = (decimal) 0.40,
                    Exampapers = new List<Exampaper>()
                    {
                        new Exampaper() {Code = "testA", Level = "H", Year = 2015},
                        new Exampaper() {Code = "testA", Level = "H", Year = 2014},
                        new Exampaper() {Code = "testA", Level = "H", Year = 2013},
                        new Exampaper() {Code = "testA", Level = "H", Year = 2012},
                        new Exampaper() {Code = "testA", Level = "H", Year = 2011}
                    }
                };

                _context.Assessments.Add(testAssessment);
                _context.Exampapers.AddRange(testAssessment.Exampapers);
                _context.SaveChanges();
            }
        }
    }
}
