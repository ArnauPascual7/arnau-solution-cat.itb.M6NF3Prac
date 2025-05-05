using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.model
{
    public class Employee
    {
        public virtual int _id { get; set; }
        public virtual string? Surname { get; set; }
        public virtual string? Job { get; set; }
        public virtual int ManagerId { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual float Salary { get; set; }
        public virtual float? Commission { get; set; }
        public virtual Department Department { get; set; }
        public virtual ISet<Client> Clients { get; set; }
    }
}
