using PwebTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PwebTP.ViewModels
{
    public class CheckListProcedureViewModel
    {
        public Checklist checklist { get; set; }
        public List<Procedures> procedures { get; set; }

        public CheckListProcedureViewModel()
        {
            procedures = new List<Procedures>();
        }
    }
}
