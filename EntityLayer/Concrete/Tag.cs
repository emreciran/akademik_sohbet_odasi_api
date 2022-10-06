using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace EntityLayer.Concrete
{
    public class Tag
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tag_ID { get; set; }

        [StringLength(50)]
        [Required]
        public string TagName { get; set; }

        public ICollection<QuestionTag> QuestionTags { get; set; }
    }
}
