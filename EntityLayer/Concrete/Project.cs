using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Concrete
{
    public class Project
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Project_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Project_Name { get; set; }

        public string Project_Details { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<ProjectCategory> ProjectCategories { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
    }
}
