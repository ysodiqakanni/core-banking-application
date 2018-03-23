using CbaSodiq.Core.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Maps
{
    class TillToUserMap : ClassMap<TillToUser>
    {
        public TillToUserMap()
        {
            Id(t => t.ID);
            Map(t => t.UserId);
            Map(t => t.TillId);

            Table("TillToUser");
        }
    }
}
