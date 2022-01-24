using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.Models
{
    public class Procedures
    {

        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProceduresId { get; set; }

        [ForeignKey("Checklist Id")]
        public string ChecklistId { get; set; }

        public Checklist Checklist { get; set; }

        [Display(Name ="Description of the procedure")]
        [Required(ErrorMessage ="Introduce the name of the procedure")]
        [StringLength(300, ErrorMessage = "The content max lenght is 300")]
        public string ProcedureName { get; set; }

        [Display(Name ="Result")]
        public Boolean ProcedureResult { get; set; }
    }
}
