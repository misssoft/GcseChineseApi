using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GcseChineseApi.ViewModels
{
    public class AssessmentViewModel
    {
        public int AssessmentId { get; set; }

        [Required]
        [StringLength(20, MinimumLength =5)]
        public string Code { get; set; }

        [Required]
        public string Level { get; set; }

        [Required]
        public string Timing { get; set; }

        [Required]
        public string Description { get; set; }

        public string DetailA { get; set; }

        public string DetailB { get; set; }

        public string DetailC { get; set; }

        public byte Marks { get; set; }

        public Decimal Percentage { get; set; }

        public IEnumerable<ExampaperViewModel> Exampapers { get; set; }

    }
}
