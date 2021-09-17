using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interface;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Model
{
    public class Zbozi :  IEntity
    {
        [Required(ErrorMessage = "Nazev knihy je vyzadovan")]
        public virtual string Nazev { get; set; }
        [Required(ErrorMessage = "Nazev knihy je vyzadovan")]

        public virtual int Cena {get; set; }
        [Required(ErrorMessage = "Nazev knihy je vyzadovan")]

        public virtual int Pocet { get; set; }
        public virtual int Id { get; set; }
        [AllowHtml]
        public virtual string Popis { get; set; }
        public virtual string Obrazek { get; set; }
        public virtual Zbozi_druh Druh { get; set; }

        

       

        
    }
}
