using cat.itb.store_PascualArnau.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.maps
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Table("DEPARTMENTS");
            Id(x => x._id);
            Map(x => x.Name).Column("name");
            Map(x => x.Loc).Column("loc");
            HasMany(x => x.Employees).AsSet()
                .KeyColumn("depid")
                .Not.LazyLoad()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join();
        }
    }
}
