using cat.itb.gestioVentas_PascualArnau;

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

        }

        public static void Exercise3()
        {

        }

        public static void Exercise4()
        {

        }

        public static void Exercise5()
        {

        }

        public static void Exercise6()
        {

        }

    }
}
