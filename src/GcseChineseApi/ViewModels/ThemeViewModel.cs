using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GcseChineseApi.Entities;

namespace GcseChineseApi.ViewModels
{
    public class ThemeViewModel
    {
        public int ThemeId { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string Notes { get; set; }

        public ICollection<TopicViewModel> Topics { get; set; }
    }
}
