using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace GcseChineseApi.Entities
{
    public class Exampaper
    {
        [ScaffoldColumn(false)]
        public int ExampaperId { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Level { get; set; }

        public string Source { get; set; }

        public int AssessmentId { get; set; }

        public Assessment Assessment { get; set; }
    }
}
