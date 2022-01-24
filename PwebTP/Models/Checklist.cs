using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.Models
{
    public class Checklist
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ChecklistId { get; set; }

        [ForeignKey("Room Id")]
        public string RoomId { get; set; }

        public Rooms Room { get; set; }


        public string Name { get; set; }


        [Display(Name = "Description")]
        public string Description { get; set; }


       
    }
}
