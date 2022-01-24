using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PwebTP.Models
{
       public enum State
       {
            [Display(Name = "Available")]
            AVAILABLE,
            [Display(Name = "Unavailable")]
            UNAVAILABLE,
          
        }
    
}
