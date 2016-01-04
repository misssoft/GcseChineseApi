using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GcseChineseApi.Entities
{
    public class Theme
    {
        public int ThemeId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Notes { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
