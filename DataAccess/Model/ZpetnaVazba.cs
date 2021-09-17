using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class ZpetnaVazba : IEntity
    {
        public virtual string Zprava { get; set; }
        public virtual EshopUzivatel eshopUzivatel { get; set; }
        public virtual int Id { get; set; }
    }
}
