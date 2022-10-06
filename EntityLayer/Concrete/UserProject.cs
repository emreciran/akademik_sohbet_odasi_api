using System;
using System.Collections.Generic;
using System.Text;

namespace EntityLayer.Concrete
{
    public class UserProject
    {
        public int Project_ID { get; set; }

        public Project Project { get; set; }

        public int User_ID { get; set; }

        public User User { get; set; }
    }
}
