using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EntityLayer.Concrete
{
    public class Question
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Question_ID { get; set; }

        [StringLength(100)]
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Vote { get; set; }

        public int View { get; set; }

        public List<Answer> Answers { get; set; }

        public List<Comment> Comments { get; set; }

        public ICollection<QuestionTag> QuestionTags { get; set; }

    }
}
