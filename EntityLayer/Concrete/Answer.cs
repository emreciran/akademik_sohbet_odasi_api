using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EntityLayer.Concrete
{
    public class Answer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Answer_ID { get; set; }

        public string Username { get; set; }

        [Required]
        public string AnswerContent { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Vote { get; set; }

        [ForeignKey("Question")]
        public int Question_ID { get; set; }

        [JsonIgnore]
        public Question Question { get; set; }
    }
}
