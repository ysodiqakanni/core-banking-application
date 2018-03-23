﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Models
{
    public class Node
    {
        public virtual int ID { get; set; }

        [Required]
        [RegularExpression(@"^[ a-zA-Z0-9_]+")]
        public virtual string Name { get; set; }

        [Required, Display(Name="Host Name")]
        [RegularExpression(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "Invalid input")]
        public virtual string HostName { get; set; }        //same as IP Address

        [Required, Display(Name = "IP Address")]
        [RegularExpression(@"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage="Invalid input")]
        public virtual string IpAddress { get; set; }

        [Required]
        [RegularExpression(@"^(6553[0-5]|655[0-2][0-9]|65[0-4](\d){2}|6[0-4](\d){3}|[1-5](\d){4}|[1-9](\d){0,3})$", ErrorMessage = "Invalid input")]
        public virtual string Port { get; set; }
    }
}
