using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PwebTP.Models
{
    public class Rooms
    {
     
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string RoomsId { get; set; }

        [ForeignKey("HostId")]
        public string HostId { get; set; }
        public ApplicationUser Host { get; set; }

        [Display(Name = "Type of Room")]
        [StringLength(50, ErrorMessage = "The content max lenght is 50")]
        [Required(ErrorMessage = "Enter the Type of the Room")]
        public string RoomType { get; set; }

        [Display(Name = "Number of Guests")]
        [Required(ErrorMessage = "Enter The Number of Guests")]
        [Range(1, 10, ErrorMessage = "The Number must be between 0 and 10")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Number of Rooms")]
        [Required(ErrorMessage = "Enter The Number of Rooms")]
        [Range(1,8, ErrorMessage = "The Number must be between 0 and 8")]
        public int NumberofRooms { get; set; }

        [Display(Name = "Number of Bathrooms")]
        [Required(ErrorMessage = "Enter The Number of Bathrooms")]
        [Range(1, 8, ErrorMessage = "The Number must be between 0 and 8")]
        public int NumberofBathrooms { get; set; }

        [Display(Name = "Price Per Night")]
        [Required(ErrorMessage = "Enter The Price per Night")]
        public double PricePerNight { get; set; }

        [Display(Name = "Title of The Room")]
        [Required(ErrorMessage = "Enter The Title of the Room")]
        [StringLength(50, ErrorMessage = "The content max lenght is 50")]
        public string Title { get; set; }

        [Display(Name = "City Name")]
        [Required(ErrorMessage = "Enter City Name")]
        [StringLength(58, ErrorMessage = "The content max lenght is 58")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string CityName { get; set; }

        [Display(Name = "Postal Code")]
        [RegularExpression(@"^\d{4}-\d{3}?$", ErrorMessage = "Invalid Zip Code")]
        [Required(ErrorMessage = "Enter the Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Enter the Street Name")]
        [StringLength(100, ErrorMessage = "The content max lenght is 100")]
        [RegularExpression(@"([A-Za-z])+( [A-Za-z]+)*", ErrorMessage = "Use letters only please")]
        public string StreetName { get; set; }

        [Display(Name = "Street Number")]
        [Required(ErrorMessage = "Enter the Street Number")]
        [Range(1,999999,ErrorMessage ="The Number must be between 1 and 999999")]
        [RegularExpression("[0-9]{1,}",ErrorMessage ="Use numbers only please")]
        public string StreetNumber { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "Enter the Country Name")]
        [StringLength(58, ErrorMessage = "The content max lenght is 58")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string CountryName { get; set; }

        [Display(Name = "Latitude")]
        [RegularExpression(@"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?)", ErrorMessage = "Invalid Latitude. Must be between [-90.0,90.0]")]
        [Required(ErrorMessage = "Enter the Latitude")]
        public string Latitude { get; set; }

        [Display(Name = "Longitude")]
        [RegularExpression(@"[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)", ErrorMessage = "Invalid Latitude. Must be between [-180.0,180.0]")]
        [Required(ErrorMessage = "Enter the Longitude")]
        public string Longitude { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "The content max lenght is 300")]
        public string Description { get; set; }

        public string CoverImgPath { get; set; }

        [NotMapped]
        [Display(Name = "Upload Cover Image")]
        public IFormFile CoverImg { get; set; }

        public State RoomState { get; set; }

        [Display(Name = "Has Wifi?")]
        public Boolean HasInternet { get; set; }

        [Display(Name = "Has Daily Cleaning?")]
        public Boolean HasDailyCleaning { get; set; }

        [Display(Name = "Has Cleaning Products?")]
        public Boolean HasCleaningProducts { get; set; }

        [Display(Name = "Has Free Parking?")]
        public Boolean HasFreeParking { get; set; }

        [Display(Name = "Has Air Conditioner?")]
        public Boolean HasAirConditioner { get; set; }

        [Display(Name = "Has Heating?")]
        public Boolean HasHeating { get; set; }

        [Display(Name = "Has Tv?")]
        public Boolean HasTv { get; set; }

      

        public Rooms()
        {
            
            RoomState = State.UNAVAILABLE;
        }

     
    }
}
