using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GcseChineseApi.Entities
{
    public class Resource
    {
        public int ResourceId { get; set; }

        public string Uri { get; set; }

        public string Notes { get; set; }

        public bool IsOfficial { get; set; }
    }
}
