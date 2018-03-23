using CbaSodiq.Core.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.Maps
{
    class ExpenseIncomeEntryMap : ClassMap<ExpenseIncomeEntry>
    {
        public ExpenseIncomeEntryMap()
        {
            Id(e => e.ID);
            Map(e => e.AccountName);
            Map(e => e.Date);
            Map(e => e.EntryType);
            Map(e => e.Amount);

            Table("ExpenseIncomeEntry");
        }
    }
}
