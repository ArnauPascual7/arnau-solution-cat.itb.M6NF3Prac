using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.model
{
    public class Department
    {
        public virtual int _id { get; set; }
        public virtual string? Name { get; set; }
        public virtual string? Loc { get; set; }
        public virtual ISet<Employee>? Employees { get; set; }
    }
}
