using cat.itb.store_PascualArnau.clieDAO;
using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.depDAO;
using cat.itb.store_PascualArnau.empDAO;
using cat.itb.store_PascualArnau.model;
using Newtonsoft.Json;
using Npgsql;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Xml.Linq;

namespace cat.itb.store_PascualArnau
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
            List<string> tables = ["CLIENTS", "DEPARTMENTS", "EMPLOYEES"];

            DropSQLTables(tables);
            RunSQLScript();
        }

        public static void RestoreFile()
        {
            SQLEmployeeImpl sqlEmpImpl = new SQLEmployeeImpl();
            FileEmployeeImpl fileEmpImpl = new FileEmployeeImpl();
            SQLClientImpl sqlClientsImpl = new SQLClientImpl();
            FileClientImpl fileClientsImpl = new FileClientImpl();
            SQLDepartmentImpl sqlDeptsImpl = new SQLDepartmentImpl();
            FileDepartmentImpl fileDeptsImpl = new FileDepartmentImpl();

            List<Employee> sqlEmps = sqlEmpImpl.SelectAll();
            fileEmpImpl.InsertAll(sqlEmps);
            List<Client> sqlClients = sqlClientsImpl.SelectAll();
            fileClientsImpl.InsertAll(sqlClients);
            List<Department> sqlDepts = sqlDeptsImpl.SelectAll();
            fileDeptsImpl.InsertAll(sqlDepts);
        }

        public static void RestoreMongoDb()
        {
            const string EmpsFileName = "employees.json";
            const string EmpsFilePath = @"..\..\..\files\" + EmpsFileName;
            const string CliesFileName = "clients.json";
            const string CliesFilePath = @"..\..\..\files\" + CliesFileName;
            const string DeptsFileName = "departments.json";
            const string DeptsFilePath = @"..\..\..\files\" + DeptsFileName;
            const string DbName = "itb";
            const string EmpsCollectionName = "employees";
            const string CliesCollectionName = "clients";
            const string DeptsCollectionName = "departments";

            LoadCollection<Employee2>(EmpsFilePath, DbName, EmpsCollectionName);
            LoadCollection<Client2>(CliesFilePath, DbName, CliesCollectionName);
            LoadCollection<Department2>(DeptsFilePath, DbName, DeptsCollectionName);
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
            const string fileName = "store.sql";
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
