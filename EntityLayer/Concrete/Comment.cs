using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EntityLayer.Concrete
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string CommentContent { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Username { get; set; }

        [ForeignKey("Question")]
        public int Question_ID { get; set; }

        [JsonIgnore]
        public Question Question { get; set; }
    }
}
