using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PwebTP.Models;

namespace PwebTP.ViewModels
{
    public class RoomDetailsViewModel
    {

        public Rooms Room { get; set; }
        public List<Reviews> RoomReviews { get;set; }
       

        public RoomDetailsViewModel()
        {
            RoomReviews = new List<Reviews>();
        
        
        }

    }
}
