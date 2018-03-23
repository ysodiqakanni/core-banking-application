using CbaSodiq.Core.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Maps
{
    class NodeMap:ClassMap<Node>
    {
        public NodeMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.HostName);
            Map(x => x.IpAddress);
            Map(x => x.Port);
        }
    }
}
