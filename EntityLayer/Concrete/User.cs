using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }

        public string  Name { get; set; }

        public string  Surname { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
    }
}
