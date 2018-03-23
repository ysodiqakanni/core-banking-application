using CbaSodiq.Core.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Maps
{
    class AtmTerminalMap : ClassMap<AtmTerminal>
    {
        public AtmTerminalMap()
        {
            Id(t => t.ID);
            Map(t => t.Name);
            Map(t => t.Code);
            Map(t => t.Location);

            Table("ATM_Terminal");
        }
    }
}
