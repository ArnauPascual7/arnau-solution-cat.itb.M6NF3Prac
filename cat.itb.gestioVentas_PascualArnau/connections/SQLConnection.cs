using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.connections
{
    public class SQLConnection
    {
        private string host = "postgresql-arnaupascual.alwaysdata.net";
        private string db = "arnaupascual_store2";
        private string user = "arnaupascual";
        private string password = "7e8@itb";

        public NpgsqlConnection conn = null;

        public NpgsqlConnection GetConnection()
        {
            NpgsqlConnection conn = new NpgsqlConnection(
                $"Host={host};Username={user};Password={password};Database={db};"
            );
            conn.Open();
            return conn;
        }
    }
}
