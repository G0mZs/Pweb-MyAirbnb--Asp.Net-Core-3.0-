using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PwebTP.Models
{
    public class Media
    {

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string MediaId { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile Image { get; set; }

        

    }
}
