using CbaSodiq.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Logic
{
    public class GlCategoryLogic
    {
        GlCategoryRepo categRepo = new GlCategoryRepo();
        public bool IsUniqueName(string name)
        {
            if (categRepo.GetByName(name) == null)
            {
                return true;
            }
            return false;
        }
    }
}
