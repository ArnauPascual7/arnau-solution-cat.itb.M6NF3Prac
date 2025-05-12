using cat.itb.gestioVentas_PascualArnau.clienteDAO;
using cat.itb.gestioVentas_PascualArnau.connections;
using cat.itb.gestioVentas_PascualArnau.model;
using Newtonsoft.Json;
using Npgsql;
using System.Diagnostics;

namespace cat.itb.gestioVentas_PascualArnau
{
    public static class InitializeDataHelper
    {
        public static void InitializeData()
        {
            RestoreSQLDb();
            RestoreFile();
            RestoreMongoDb();
        }

        public static void RestoreSQLDb()
        {
            List<string> tables = ["CLIENTE", "comercial", "pedido"];

            DropSQLTables(tables);
            RunSQLScript();
        }

        public static void RestoreFile()
        {
            SQLClienteImpl sqlClienteImpl = new SQLClienteImpl();
            FileClienteImpl fileClienteImpl = new FileClienteImpl();

            List<Cliente> sqlClients = sqlClienteImpl.SelectAll();
            fileClienteImpl.InsertAll(sqlClients);
        }

        public static void RestoreMongoDb()
        {
            const string CliesFileName = "cliente.json";
            const string CliesFilePath = @"..\..\..\files\" + CliesFileName;
            const string DbName = "itb";
            const string CliesCollectionName = "cliente";

            DropCollections(DbName, new List<string> { CliesCollectionName });
            LoadCollection<Cliente>(CliesFilePath, DbName, CliesCollectionName);
        }

        public static void DropSQLTables(List<string> tables)
        {
            SQLConnection db = new SQLConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                NpgsqlCommand cmd = new NpgsqlCommand() { Connection = conn };

                foreach (string table in tables)
                {
                    cmd.CommandText = $"DROP TABLE IF EXISTS {table} CASCADE";
                    cmd.ExecuteNonQuery();

                    Console.WriteLine($"Taula SQL {table} Eliminada");
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTotes les taules de la base de dades SQL han estat eliminades");
            Console.ResetColor();
        }

        public static void RunSQLScript()
        {
            const string fileName = "ventas.sql";
            const string filePath = @"..\..\..\files\" + fileName;

            Debug.WriteLine("?: Sql Script Path -> " + Path.GetFullPath(filePath));

            SQLConnection db = new SQLConnection();
            using (NpgsqlConnection conn = db.GetConnection())
            {
                string script = File.ReadAllText(filePath);
                NpgsqlCommand cmd = new NpgsqlCommand()
                {
                    Connection = conn,
                    CommandText = script
                };
                cmd.ExecuteNonQuery();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nScript Executat, base de dades SQL restaurada");
            Console.ResetColor();
        }

        public static void DropCollections(string dbName, List<string> collections)
        {
            var db = MongoConnection.GetDatabase(dbName);
            foreach (string collection in collections)
            {
                db.DropCollection(collection);
                Console.WriteLine($"Colecció {collection} eliminada");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nTotes les col·leccions de la base de dades MongoDB han estat eliminades");
            Console.ResetColor();
        }

        public static void LoadCollection<T>(string filePath, string dbName, string collectionName)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            var db = MongoConnection.GetDatabase(dbName);
            db.DropCollection(collectionName);

            Console.WriteLine($"\nColecció {collectionName} eliminada");

            var collection = db.GetCollection<T>(collectionName);

            using (StreamReader reader = fileInfo.OpenText())
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    T? register = JsonConvert.DeserializeObject<T>(line);
                    if (register != null) collection.InsertOne(register);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTots els registres de la collecció {collectionName} insertats");
            Console.ResetColor();
        }

    }
}
