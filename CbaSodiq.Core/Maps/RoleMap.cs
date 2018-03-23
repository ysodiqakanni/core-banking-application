using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using CbaSodiq.Core.Models;

namespace CbaSodiq.Core.Maps
{
    class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(r => r.ID);
            Map(r => r.Name);
        }
    }
}
