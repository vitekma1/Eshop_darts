using DataAccess.Model;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
    public class EshopUzivatelDao : DaoBase<EshopUzivatel>
    {
        public EshopUzivatel GetByLoginAndPassword(string login, string heslo)
        {
           return session.CreateCriteria<EshopUzivatel>()
                .Add(Restrictions.Eq("Login", login))
                .Add(Restrictions.Eq("Heslo", heslo))
                .UniqueResult<EshopUzivatel>();
        }
        public EshopUzivatel GetByLogin(string login)
        {
            return session.CreateCriteria<EshopUzivatel>()
                 .Add(Restrictions.Eq("Login", login))               
                 .UniqueResult<EshopUzivatel>();
        }
        
    }
}
