using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GcseChineseApi.ViewModels
{
    public class ExampaperViewModel
    {
        public int ExampaperId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Level { get; set; }

        public string Source { get; set; }
    }
}
