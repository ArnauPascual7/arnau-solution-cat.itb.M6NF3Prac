using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.connections
{
    public class SessionFactorySQLConnection
    {
        private static string ConnectionString =
    "Server=postgresql-arnaupascual.alwaysdata.net;" +
    "Port=5432;" +
    "Database=arnaupascual_ventas;" +
    "User Id=arnaupascual;" +
    "Password=7e8@itb;";

        private static ISessionFactory? session;

        public static ISessionFactory CreateSession()
        {
            if (session != null)
                return session;

            IPersistenceConfigurer configDB = PostgreSQLConfiguration.PostgreSQL82.ConnectionString(ConnectionString);

            var configMap = Fluently
                .Configure()
                .Database(configDB)
                // Prova <SessionFactorySQLConnection> // Previous <Program>
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<SessionFactorySQLConnection>());

            session = configMap.BuildSessionFactory();

            return session;
        }

        public static ISession Open()
        {
            return CreateSession().OpenSession();
        }
    }
}
