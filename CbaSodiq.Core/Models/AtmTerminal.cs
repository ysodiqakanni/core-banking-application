using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Models
{
    public class AtmTerminal
    {
        public virtual int ID { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [RegularExpression(@"^[ a-zA-Z0-9_]+")]
        public virtual string Name { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{8}", ErrorMessage ="Code must be of any 8 digits")]
        public virtual string Code { get; set; }

        public virtual string Location { get; set; }
    }
}
