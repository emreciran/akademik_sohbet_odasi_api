using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akademik_sohbet_odasi_api.Dto
{
    public class QuestionDto
    {
        public int Question_ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int Vote { get; set; }

        public int View { get; set; }
    }
}
