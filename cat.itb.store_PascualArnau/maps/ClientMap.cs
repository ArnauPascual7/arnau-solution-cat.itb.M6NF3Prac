using cat.itb.store_PascualArnau.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.maps
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table("CLIENTS");
            Id(x => x._id).GeneratedBy.Assigned();
            Map(x => x.Name).Column("name");
            Map(x => x.Address).Column("address");
            Map(x => x.City).Column("city");
            Map(x => x.St).Column("st");
            Map(x => x.Zipcode).Column("zipcode");
            Map(x => x.Area).Column("area");
            Map(x => x.Phone).Column("phone");
            Map(x => x.Credit).Column("credit");
            Map(x => x.Comments).Column("comments");
            References(x => x.Employee)
                .Column("empid")
                .Not.LazyLoad()
                .Fetch.Join();
        }
    }
}
