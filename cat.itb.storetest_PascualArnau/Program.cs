using cat.itb.store_PascualArnau;
using cat.itb.store_PascualArnau.clieDAO;
using cat.itb.store_PascualArnau.empDAO;
using cat.itb.store_PascualArnau.model;
using StoreLib = cat.itb.store_PascualArnau;
namespace cat.itb.storetest_PascualArnau
{
    public class Program
    {
        // S'ha d'eliminar i tornar a afegir la dependècia al canviar de màquina
        public static void Main(string[] args)
        {
            DisplayMenu();
        }

        public static void DisplayMenu()
        {
            const string Menu = "Menú:" +
                "\n[0] Restaurar-ho tot (PosgreSQL, Json Files, MongoDb)" +
                "\n[1] Eliminar-ho tot (PosgreSQL, MongoDb)" +
                "\n[3] Exercici 3" +
                "\n[4] Exercici 4" +
                "\n[5] Exercici 5" +
                "\n[6] Exercici 6" +
                "\n[7] Exercici 7" +
                "\n[8] Exercici 8" +
                "\n[9] Exercici 9" +
                "\n[10] Exercici 10" +
                "\n[11] Exercici 11" +
                "\n[12] Exercici 12" +
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
                case "7":
                    Exercise7();
                    break;
                case "8":
                    Exercise8();
                    break;
                case "9":
                    Exercise9();
                    break;
                case "10":
                    Exercise10();
                    break;
                case "11":
                    Exercise11();
                    break;
                case "12":
                    Exercise12();
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
            List<string> tables = ["CLIENTS", "DEPARTMENTS", "EMPLOYEES"];
            List<string> collections = ["employees", "departments", "clients"];

            InitializeDataHelper.DropSQLTables(tables);
            InitializeDataHelper.DropCollections("itb", collections);
        }

        public static void Exercise3()
        {
            Console.WriteLine("Exercici 3: Afegir els empleats del PostgreSQL al fitxer employees.json");

            SQLEmployeeImpl sqlEmployee = new SQLEmployeeImpl();
            FileEmployeeImpl fileEmployee = new FileEmployeeImpl();

            List<Employee> emps = sqlEmployee.SelectAll();
            fileEmployee.InsertAll(emps);
        }

        public static void Exercise4()
        {
            Console.WriteLine("Exercici 4: Afegir els clients del PostgreSQL al fitxer clients.json");

            SQLClientImpl sqlClient = new SQLClientImpl();
            FileClientImpl fileClient = new FileClientImpl();

            List<Client> clies = sqlClient.SelectAll();
            fileClient.InsertAll(clies);
        }

        public static void Exercise5()
        {
            Console.WriteLine("Exercici 5: Afegir els empleats del fitxer a MongoDb");

            FileEmployeeImpl fileEmployee = new FileEmployeeImpl();
            MongoEmployeeImpl mongoEmployee = new MongoEmployeeImpl();

            List<Employee> emps = fileEmployee.SelectAll();
            List<Employee2> emps2 = new List<Employee2>();

            foreach (var emp in emps)
            {
                emps2.Add(ModelConverterHelper.ConvertToEmployee2(emp));
            }

            mongoEmployee.InsertAll(emps2);
        }

        public static void Exercise6()
        {
            Console.WriteLine("Exercici 6: Afegir els clients del fitxer a MongoDb");

            FileClientImpl fileClient = new FileClientImpl();
            MongoClientImpl mongoClient = new MongoClientImpl();

            List<Client> clies = fileClient.SelectAll();
            List<Client2> clies2 = new List<Client2>();

            foreach (var clie in clies)
            {
                clies2.Add(ModelConverterHelper.ConvertToClient2(clie));
            }

            mongoClient.InsertAll(clies2);
        }

        public static void Exercise7()
        {
            Console.WriteLine("Exercici 7: Obtenir de MongoDb un empleat per al seu id" +
                "\nModificar el salari al MongoDb i PosgreSQL i aplicar-ho al arxiu");

            SQLEmployeeImpl sqlEmployee = new SQLEmployeeImpl();
            FileEmployeeImpl fileEmployee = new FileEmployeeImpl();
            MongoEmployeeImpl mongoEmployee = new MongoEmployeeImpl();

            Employee2 emp = mongoEmployee.Select(7499);

            Console.WriteLine($"Empleat -> Id: {emp._id}, Cognom: {emp.Surname}, Feina: {emp.Job}, Manager {emp.ManagerId}, " +
                $"Data d'inici: {emp.StartDate}, Salari: {emp.Salary}, Comissió: {emp.Commission}, Departament: {emp.Department}");

            emp.Salary = 1800;

            mongoEmployee.Update(emp);
            sqlEmployee.Update(ModelConverterHelper.ConvertToEmployee(emp));
            fileEmployee.InsertAll(sqlEmployee.SelectAll());
        }

        public static void Exercise8()
        {
            Console.WriteLine("Exercici 7: Obtenir de MongoDb un client per al seu id" +
                "\nModificar el telefon al MongoDb i PosgreSQL i aplicar-ho al arxiu");

            SQLClientImpl sqlClient = new SQLClientImpl();
            FileClientImpl fileClient = new FileClientImpl();
            MongoClientImpl mongoClient = new MongoClientImpl();

            Client2 clie = mongoClient.Select(101);

            Console.WriteLine($"Client -> Id: {clie._id}, Nom: {clie.Name}, Adreça: {clie.Address}, Ciutat: {clie.City}, " +
                $"St: {clie.St}, Codi postal: {clie.Zipcode}, Area: {clie.Area}, Telèfon: {clie.Phone}, Crèdit: {clie.Credit}, " +
                $"Commentaris: {clie.Comments}");

            clie.Phone = "555-2331";

            mongoClient.Update(clie);
            sqlClient.Update(ModelConverterHelper.ConvertToClient(clie));
            fileClient.InsertAll(sqlClient.SelectAll());
        }

        public static void Exercise9()
        {
            Console.WriteLine("Exercici 9: Insertar un nou empleat a PosgreSQL i MongoDB i aplicar-ho al arxiu");

            SQLEmployeeImpl sqlEmployee = new SQLEmployeeImpl();
            FileEmployeeImpl fileEmployee = new FileEmployeeImpl();
            MongoEmployeeImpl mongoEmployee = new MongoEmployeeImpl();

            Employee2 emp = new Employee2
            {
                _id = 33,
                Surname = "PASCUAL",
                Job = "IT",
                ManagerId = 100,
                StartDate = DateTime.Now,
                Salary = 2000,
                Commission = 0,
                Department = 10
            };

            sqlEmployee.Insert(ModelConverterHelper.ConvertToEmployee(emp));
            mongoEmployee.Insert(emp);
            fileEmployee.InsertAll(sqlEmployee.SelectAll());
        }

        public static void Exercise10()
        {
            Console.WriteLine("Exercici 10: Mostrar el nom i localització del empleat amb id 7788");

            SQLEmployeeImpl sqlEmployee = new SQLEmployeeImpl();

            Employee emp = sqlEmployee.Select(7788);

            Console.WriteLine($"Empleat: {emp._id} {emp.Surname} -> Departament: {emp.Department.Name} {emp.Department.Loc}");
        }

        public static void Exercise11()
        {
            Console.WriteLine("Exercici 11: Mostrar quants clients té l'empleat amb id 7844 i eliminar aquests clients de PosgreSQL i MongoDB i aplicar-ho al arxiu");

            SQLClientImpl sqlClient = new SQLClientImpl();
            FileClientImpl fileClient = new FileClientImpl();
            MongoClientImpl mongoClient = new MongoClientImpl();

            List<Client> clies = sqlClient.SelectByEmpId(7844);

            Console.WriteLine($"Clients de l'empleat amb id 7844: {clies.Count}");

            Console.WriteLine("\nQuantitat de clients abans d'eliminar l'empleat:");
            Console.WriteLine($"PosgreSQL: {sqlClient.SelectAll().Count}");
            Console.WriteLine($"Fitxer: {fileClient.SelectAll().Count}");
            Console.WriteLine($"MongoDB: {mongoClient.SelectAll().Count}\n");

            clies.ForEach(clie =>
            {
                sqlClient.Delete(clie._id);
                mongoClient.Delete(clie._id);
            });
            fileClient.InsertAll(sqlClient.SelectAll());

            Console.WriteLine("\nQuantitat de clients després d'eliminar l'empleat:");
            Console.WriteLine($"PosgreSQL: {sqlClient.SelectAll().Count}");
            Console.WriteLine($"Fitxer: {fileClient.SelectAll().Count}");
            Console.WriteLine($"MongoDB: {mongoClient.SelectAll().Count}");
        }

        public static void Exercise12()
        {
            Console.WriteLine("Exercici 12: Mostrar els clients que té assignat l'empleat ARROYO");

            SQLClientImpl sqlClient = new SQLClientImpl();
            MongoClientImpl mongoClient = new MongoClientImpl();

            List<Client> clies = sqlClient.SelectByEmpSurname("ARROYO");
            List<Client2> clies2 = mongoClient.SelectByEmpSurname("ARROYO");

            Console.WriteLine($"\nClients assignats a ARROYO en PosgreSQL: {clies.Count}");
            clies.ForEach(c => Console.WriteLine($"Client: {c._id}, Telèfon: {c.Phone}"));

            Console.WriteLine($"\nClients assignats a ARROYO en MongoDB: {clies2.Count}");
            clies2.ForEach(c => Console.WriteLine($"Client: {c._id}, Telèfon: {c.Phone}"));
        }
    }
}
