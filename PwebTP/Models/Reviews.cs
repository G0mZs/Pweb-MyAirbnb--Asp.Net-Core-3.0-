using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.Models
{
    public class Reviews
    {

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ReviewsId { get; set; }

        [ForeignKey("Room")]
        public string RoomId { get; set; }

        public Rooms Room { get; set; }

        [ForeignKey("ReviewerId")]
        public string ReviewerId { get; set; }

        public ApplicationUser Reviewer { get; set; }

      


        public int TotalRating { get; set; }

        [Display(Name = "Location Rating")]
        [Range(0, 5, ErrorMessage = "Enter a rating from 0 to 5")]
        [Required(ErrorMessage ="Enter a Rating from 0 to 5")]
        public double LocationRating { get; set; }

      
        [Range(0, 5, ErrorMessage = "Enter a rating from 0 to 5")]
        [Required(ErrorMessage = "Enter a Rating from 0 to 5")]
        public double UserRating { get; set; }

        [Display(Name = "Employees Rating")]
        [Range(0, 5, ErrorMessage = "Enter a rating from 0 to 5")]
        [Required(ErrorMessage = "Enter a Rating from 0 to 5")]
        public double EmployeeRating { get; set; }

        [Display(Name = "Room Rating")]
        [Range(0, 5, ErrorMessage = "Enter a rating from 0 to 5")]
        [Required(ErrorMessage = "Enter a Rating from 0 to 5")]
        public double RoomRating { get; set; }

        [Display(Name ="Comment")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        
      
    }
}
