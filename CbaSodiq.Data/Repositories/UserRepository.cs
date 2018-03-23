using CbaSodiq.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace CbaSodiq.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public User GetByUsername(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<User>().Where(u => u.Username.ToLower().Equals(name.ToLower())).FirstOrDefault();
            }
        }
        public User GetByEmail(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<User>().Where(u => u.Email.ToLower().Equals(name.ToLower())).FirstOrDefault();
            }
        }
        
        public User FindUser(string username, string passwordHash)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<User>().Where(u => u.Username.ToLower().Equals(username.ToLower()) && u.PasswordHash.ToLower().Equals(passwordHash.ToLower())).FirstOrDefault();
            }
        }
        public List<User> GetAllTellers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Query<User>().Where(u => u.Role.ID == 2).ToList();   //teller role has id of 2
            }
        }

        
       
    }
}
