using CbaSodiq.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Data.Repositories
{
    public class ConfigurationRepository : BaseRepository<AccountConfiguration>
    {
        public void Initialize()
        {
            if (this.GetCount() < 1)    //no configuration yet
            {
                this.Insert(new AccountConfiguration() { FinancialDate = new DateTime(2016, 06, 12) });
            }
        }
    }
}
