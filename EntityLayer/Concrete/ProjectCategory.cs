using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Concrete
{
    public class ProjectCategory
    {
        public int Project_ID { get; set; }

        public Project Project { get; set; }

        public int Category_ID { get; set; }

        public Category Category { get; set; }
    }
}
