using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.Models
{
    public class Reservations
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ReservationsId { get; set; }

    
        [ForeignKey("Client")]
        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        [ForeignKey("Room Id")]
        public string RoomId { get; set; }

        public Rooms Room { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Enter the Reservation Start Date")]
        public DateTime BeginsAt { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "Enter the Reservation End Date")]
        public DateTime EndsAt { get; set; }

        [Display(Name ="Number of Guests")]
        [Required(ErrorMessage ="Enter the Number of Guests")]
        public int GuestsNumber { get; set; }

        [Display(Name = "Total Cost")]
        public double TotalCost { get; set; }

        [Display(Name = "State")]
        public string ReservationState { get; set; }

       
    }
}
