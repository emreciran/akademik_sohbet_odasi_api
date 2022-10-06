using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Concrete
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Category_ID { get; set; }

        [Required]
        public string Category_Name { get; set; }

        public string Category_Details { get; set; }

        public bool Category_Status { get; set; }

        public ICollection<ProjectCategory> ProjectCategories { get; set; }
    }
}
