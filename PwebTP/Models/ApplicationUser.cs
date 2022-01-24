using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace PwebTP.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Role { get; set; }

        [ForeignKey("Manager")]
        public string ManagerId { get; set; }
        
        public ApplicationUser Manager { get; set; }

        [NotMapped]
        public List<Reviews> Reviews { get; set; }

        public ApplicationUser()
        {
            Reviews = new List<Reviews>();
        }

    }
}
