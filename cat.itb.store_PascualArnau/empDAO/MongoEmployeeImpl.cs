using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public class MongoEmployeeImpl : EmployeeDAO
    {
        private const string dbName = "itb";
        private const string collectionName = "employees";

        /// <summary>
        /// Delete an Employee from the mongoDb collection
        /// </summary>
        /// <param name="empId">Id of the Employee to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int empId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee>(collectionName);

            bool correct;

            var deleteFilter = Builders<Employee>.Filter.Eq("Id", empId);

            var depDeleted = collection.DeleteOne(deleteFilter);

            if (depDeleted.DeletedCount != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {empId} eliminat");

                correct = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en eliminar el registre de {collectionName} amb id {empId}");

                correct = false;
            }
            Console.ResetColor();
            
            return correct;
        }

        /// <summary>
        /// Delete all Employees from mongoDb collection
        /// </summary>
        public void DeleteAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            database.DropCollection(collectionName);
        }

        /// <summary>
        /// Insert one Employee to the mongoDb collection
        /// </summary>
        /// <param name="emp">Employee to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Employee emp)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee>(collectionName);

            bool correct;
            try
            {
                collection.InsertOne(emp);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nRegistre insertat correctament en {dbName}");

                correct = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en insertar el registre de {dbName}");

                correct = false;
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Insert many Employees to the mongoDb collection
        /// </summary>
        /// <param name="emps">Employees to insert</param>
        public void InsertAll(List<Employee> emps)
        {
            DeleteAll();

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee>(collectionName);

            try
            {
                collection.InsertMany(emps);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nRegistres insertats correctament en {collectionName} ");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError en insertar els registres de {collectionName}");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Select one Employee from mongoDb collection
        /// </summary>
        /// <param name="empId">Id of the Employee</param>
        /// <returns>Selected Employee</returns>
        public Employee Select(int empId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee>(collectionName);

            var emp = collection.AsQueryable<Employee>()
                        .Where(d => d._id == empId)
                        .Single();

            return emp;
        }

        /// <summary>
        /// Select all Employees from mongoDb collection
        /// </summary>
        /// <returns>List of selected Employees</returns>
        public List<Employee> SelectAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee>(collectionName);

            var emps = collection.AsQueryable<Employee>().ToList();

            return emps;
        }

        /// <summary>
        /// Update one Employee of the mongoDb collection
        /// </summary>
        /// <param name="emp">Employee to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Employee emp)
        {
            if (Delete(emp._id) && Insert(emp))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {emp._id} actualitzat");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en actualitzar el registre de {collectionName} amb id {emp._id}");

                return false;
            }
            Console.ResetColor();

            return true;
        }
    }
}
