using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Objednavka : IEntity
    {
        public virtual int Cena { get; set; }
        public virtual EshopUzivatel eshopUzivatel { get; set; }
        public virtual Zbozi zbozi { get; set; }
        public virtual int Id { get; set; }
        public virtual int Platnost { get; set; }
        public virtual int Ks { get; set; }

    }
}
