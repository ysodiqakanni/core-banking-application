using CbaSodiq.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CbaSodiq.Data.Repositories
{
    public class AtmTerminalRepo : BaseRepository<AtmTerminal>
    {
        public bool isUniqueName(string name)
        {
            bool flag = true;
            if (GetAll().Any(n => n.Name.ToLower().Equals(name.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueName(string oldName, string newName)
        {
            bool flag = true;
            if (!oldName.ToLower().Equals(newName.ToLower()))
            {
                if (GetAll().Any(n => n.Name.ToLower().Equals(newName.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }
        public bool isUniqueCode(string code)
        {
            bool flag = true;
            if (GetAll().Any(n => n.Code.ToLower().Equals(code.ToLower())))
            {
                flag = false;
            }
            return flag;
        }
        public bool isUniqueCode(string oldCode, string newCode)
        {
            bool flag = true;
            if (!oldCode.ToLower().Equals(newCode.ToLower()))
            {
                if (GetAll().Any(n => n.Code.ToLower().Equals(newCode.ToLower())))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public bool isValidTerminal(string terminalCode)
        {           
            return GetAll().Any(t => t.Code == terminalCode);
        }

    }
}
