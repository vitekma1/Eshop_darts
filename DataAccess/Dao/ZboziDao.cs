using DataAccess.Model;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dao
{
   public class ZboziDao : DaoBase<Zbozi>
    {
        public ZboziDao() : base()
        {
        }

        public IList<Zbozi> GetZboziPaged(int count, int page, out int totalZbozi)
        {
            totalZbozi = session.CreateCriteria<Zbozi>().SetProjection(Projections.RowCount()).UniqueResult<int>();

            return session.CreateCriteria<Zbozi>().AddOrder(Order.Asc("Nazev")).SetFirstResult((page - 1) * count).SetMaxResults(count).List<Zbozi>();
        }
        public IList<Zbozi> SearchZbozi(string phrase)
        {
            return session.CreateCriteria<Zbozi>()
                .Add(Restrictions.Like("Nazev", string.Format("%{0}%", phrase)))
                .List<Zbozi>();
        }
        public IList<Zbozi> GetZboziInCategoryId(int id)
        {
            return session.CreateCriteria<Zbozi>()
                .CreateAlias("Druh", "cat")
                .Add(Restrictions.Eq("cat.Id", id))
                .List<Zbozi>();
        }
    }

    
}
