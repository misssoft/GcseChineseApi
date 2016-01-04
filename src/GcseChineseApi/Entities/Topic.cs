using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GcseChineseApi.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public ICollection<Resource> Resources { get; set; }
    }
}
