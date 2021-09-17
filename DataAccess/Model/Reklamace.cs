using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Reklamace : IEntity
    {
        public virtual string Popis { get; set; }
        public virtual EshopUzivatel eshopUzivatel { get; set; }
        public virtual int Id { get; set; }
        public virtual Objednavka objednavka { get; set; }
    }
}
