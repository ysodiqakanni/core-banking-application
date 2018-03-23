using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CbaSodiq.Core.Models
{
    public class User
    {
        public virtual int ID { get; set; }
        public virtual string Username { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        

        [DataType(DataType.EmailAddress)]
        [Display(Name="Email Address")]
        public virtual string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public virtual string PhoneNumber { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Role Role { get; set; }

    }
}
