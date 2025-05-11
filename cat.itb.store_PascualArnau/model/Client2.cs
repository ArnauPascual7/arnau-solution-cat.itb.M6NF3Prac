using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.model
{
    public class Client2
    {
        public virtual int _id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string? St { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual int Area { get; set; }
        public virtual string? Phone { get; set; }
        public virtual int? Employee { get; set; }
        public virtual float Credit { get; set; }
        public virtual string? Comments { get; set; }
    }
}
