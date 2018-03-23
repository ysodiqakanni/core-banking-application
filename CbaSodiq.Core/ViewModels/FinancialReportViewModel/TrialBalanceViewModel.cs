﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Core.ViewModels.FinancialReportViewModel
{
    public class TrialBalanceViewModel
    {
        public string SubCategory { get; set; }
        public string MainCategory { get; set; }
        public string AccountName { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
    }//end trial balance
}
