using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Concrete
{
    public class QuestionTag
    {
        public int Question_ID { get; set; }

        public Question Question { get; set; }

        public int Tag_ID { get; set; }

        public Tag Tag { get; set; }
    }
}
