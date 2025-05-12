using cat.itb.gestioVentas_PascualArnau.model;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.maps
{
    public class ClienteMap : ClassMap<Cliente>
    {
        public ClienteMap()
        {
            Table("cliente");
            Id(x => x._id).GeneratedBy.Assigned();
            Map(x => x.Nombre).Column("nombre");
            Map(x => x.Apellido1).Column("apellido1");
            Map(x => x.Apellido2).Column("apellido2");
            Map(x => x.Ciudad).Column("ciudad");
            Map(x => x.Categoria).Column("categoria");
        }
    }
}
