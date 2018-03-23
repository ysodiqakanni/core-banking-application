using CbaSodiq.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Logic
{
    public class RoleLogic
    {
        RoleRepository roleRepo = new RoleRepository();
        public bool isRoleNameExists(string name)
        {
            if (roleRepo.GetByName(name) == null)
            {
                return false;
            }
            return true;
        }
    }
}
