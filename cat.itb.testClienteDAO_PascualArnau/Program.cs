using cat.itb.gestioVentas_PascualArnau;
using cat.itb.gestioVentas_PascualArnau.clienteDAO;
using cat.itb.gestioVentas_PascualArnau.model;

namespace cat.itb.testClienteDAO_PascualArnau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DisplayMenu();
        }

        public static void DisplayMenu()
        {
            const string Menu = "Menú:" +
                "\n[0] Restaurar-ho tot (PosgreSQL, Json Files, MongoDb)" +
                "\n[1] Eliminar-ho tot (PosgreSQL, MongoDb)" +
                "\n[2] Exercici 2" +
                "\n[3] Exercici 3" +
                "\n[4] Exercici 4" +
                "\n[5] Exercici 5" +
                "\n[6] Exercici 6" +
                "\n[ex] Sortir" +
                "\n";

            Console.WriteLine(Menu);

            string? option = Console.ReadLine();
            Console.Clear();

            bool exit = false;
            switch (option)
            {
                case "0":
                    RestoreAll();
                    break;
                case "1":
                    DeleteAll();
                    break;
                case "2":
                    Exercise2();
                    break;
                case "3":
                    Exercise3();
                    break;
                case "4":
                    Exercise4();
                    break;
                case "5":
                    Exercise5();
                    break;
                case "6":
                    Exercise6();
                    break;
                case "ex" or "EX" or "Ex":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opció no vàlida");
                    break;
            }

            if (!exit)
            {
                Console.Write("\nPrem ENTER per tornar al menú");
                Console.ReadLine();
                Console.Clear();

                DisplayMenu();
            }
            else
            {
                Console.WriteLine("Fi del programa");
            }
        }

        public static void RestoreAll()
        {
            InitializeDataHelper.InitializeData();
        }

        public static void DeleteAll()
        {
            List<string> tables = ["CLIENTE"];
            List<string> collections = ["cliente"];

            InitializeDataHelper.DropSQLTables(tables);
            InitializeDataHelper.DropCollections("itb", collections);
        }

        public static void Exercise2()
        {
            Console.WriteLine("Exercici 2: A partir dels clients a SQL passar-ho al fitxer cliente.json");

            InitializeDataHelper.RestoreSQLDb();

            SQLClienteImpl sqlClienteImpl = new SQLClienteImpl();
            FileClienteImpl fileClienteImpl = new FileClienteImpl();

            List<Cliente> clies = sqlClienteImpl.SelectAll();

            fileClienteImpl.InsertAll(clies);
        }

        public static void Exercise3()
        {
            Console.WriteLine("Exercici 3: A partir dels clients al fitxer cliente.json passar-ho a MongoDb");

            FileClienteImpl fileClienteImpl = new FileClienteImpl();
            MongoClienteImpl mongoClienteImpl = new MongoClienteImpl();

            List<Cliente> clies = fileClienteImpl.SelectAll();

            mongoClienteImpl.InsertAll(clies);
        }

        public static void Exercise4()
        {
            Console.WriteLine("Exercici 4: Mostrar de MongoDb un client per el seu Id i modificar-li la ciutat");

            SQLClienteImpl sqlClienteImpl = new SQLClienteImpl();
            FileClienteImpl fileClienteImpl = new FileClienteImpl();
            MongoClienteImpl mongoClienteImpl = new MongoClienteImpl();

            Cliente clie = mongoClienteImpl.Select(9);

            Console.WriteLine($"Client -> Id: {clie._id}, Nom: {clie.Nombre}, Primer Cognom: {clie.Apellido1}," +
                $"Segon Cognom: {clie.Apellido2}, Categoria: {clie.Categoria}, Ciutat: {clie.Ciudad}");

            clie.Ciudad = "Lleida";

            mongoClienteImpl.Update(clie);
            sqlClienteImpl.Update(clie);
            fileClienteImpl.InsertAll(sqlClienteImpl.SelectAll());

            Cliente modClie = mongoClienteImpl.Select(9);

            Console.WriteLine($"Client Modificat -> Id: {modClie._id}, Nom: {modClie.Nombre}, Primer Cognom: {modClie.Apellido1}," +
                $"Segon Cognom: {modClie.Apellido2}, Categoria: {modClie.Categoria}, Ciutat: {modClie.Ciudad}");
        }

        public static void Exercise5()
        {
            Console.WriteLine("Exercici 5: Insertar un nou client a PosgreSQL i aplicar-ho a MongoDb i el fitxer");

            SQLClienteImpl sqlClienteImpl = new SQLClienteImpl();
            FileClienteImpl fileClienteImpl = new FileClienteImpl();
            MongoClienteImpl mongoClienteImpl = new MongoClienteImpl();

            Cliente newClie = new Cliente()
            {
                _id = 33,
                Nombre = "Arnau",
                Apellido1 = "Pascual",
                Apellido2 = "Carretero",
                Ciudad = "Barcelona",
                Categoria = 999
            };

            sqlClienteImpl.Insert(newClie);
            fileClienteImpl.InsertAll(sqlClienteImpl.SelectAll());
            mongoClienteImpl.DeleteAll();
            mongoClienteImpl.InsertAll(fileClienteImpl.SelectAll());
        }

        public static void Exercise6()
        {
            Console.WriteLine("Exercici 6: Obtenir un client per al seu nom i eliminar-lo de MongoDB i aplicar-ho a PosgreSQL i al fitxer");

            SQLClienteImpl sqlClienteImpl = new SQLClienteImpl();
            FileClienteImpl fileClienteImpl = new FileClienteImpl();
            MongoClienteImpl mongoClienteImpl = new MongoClienteImpl();

            Cliente clie = mongoClienteImpl.SelectByName("Quim");

            Console.WriteLine($"Client -> Id: {clie._id}, Nom: {clie.Nombre}, Primer Cognom: {clie.Apellido1}," +
                $"Segon Cognom: {clie.Apellido2}, Categoria: {clie.Categoria}, Ciutat: {clie.Ciudad}");

            mongoClienteImpl.Delete(clie._id);
            sqlClienteImpl.Delete(clie._id);
            fileClienteImpl.InsertAll(sqlClienteImpl.SelectAll());
        }
    }
}
