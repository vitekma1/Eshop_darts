using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace DataAccess.Model
{
   public class EshopUzivatel : MembershipUser, IEntity

    {

        public virtual string Jmeno { get; set; }
        public virtual string Prijmeni { get; set; }
        public virtual string Login { get; set; }
        public virtual string Heslo { get; set; }
        public virtual string Email { get; set; }
        public virtual string Adresa { get; set; }
        public virtual int PSC { get; set; }
        public virtual int Id { get; set; }
        public virtual EshopRole Role { get; set; }
    }
}
