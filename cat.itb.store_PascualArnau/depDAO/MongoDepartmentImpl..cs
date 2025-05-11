using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using MongoDB.Driver;

namespace cat.itb.store_PascualArnau.depDAO
{
    public class MongoDepartmentImpl : IDepartmentDAO<Department2>
    {
        private const string dbName = "itb";
        private const string collectionName = "departments";

        /// <summary>
        /// Delete an Department from the mongoDb collection
        /// </summary>
        /// <param name="deptId">Id of the Department to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int deptId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Department2>(collectionName);

            bool correct;

            var deleteFilter = Builders<Department2>.Filter.Eq("_id", deptId);

            var depDeleted = collection.DeleteOne(deleteFilter);

            if (depDeleted.DeletedCount != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {deptId} eliminat");

                correct = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en eliminar el registre de {collectionName} amb id {deptId}");

                correct = false;
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Delete all Department from mongoDb collection
        /// </summary>
        public void DeleteAll()
        {
            try
            {
                var database = MongoConnection.GetDatabase(dbName);
                database.DropCollection(collectionName);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Tots els registres de {collectionName} eliminats");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"?: Error deleting in {collectionName} -> {ex}");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en eliminar els registres de {collectionName}");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Insert one Department to the mongoDb collection
        /// </summary>
        /// <param name="dept">Department to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Department2 dept)
        {

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Department2>(collectionName);

            bool correct;
            try
            {
                collection.InsertOne(dept);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nRegistre insertat correctament en {dbName}");

                correct = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"?: Error inserting in {collectionName} -> {ex}");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en insertar el registre de {dbName}");

                correct = false;
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Insert many Department to the mongoDb collection
        /// </summary>
        /// <param name="depts">Department to insert</param>
        public void InsertAll(List<Department2> depts)
        {
            //DeleteAll();

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Department2>(collectionName);

            try
            {
                collection.InsertMany(depts);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nRegistres insertats correctament en {collectionName} ");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"?: Error inserting in {collectionName} -> {ex}");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError en insertar els registres de {collectionName}");
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Select one Department from mongoDb collection
        /// </summary>
        /// <param name="deptId">Id of the Department</param>
        /// <returns>Selected Department</returns>
        public Department2 Select(int deptId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Department2>(collectionName);

            var dept = collection.AsQueryable<Department2>()
                        .Where(d => d._id == deptId)
                        .Single();

            return dept;
        }

        /// <summary>
        /// Select all Department from mongoDb collection
        /// </summary>
        /// <returns>List of selected Department</returns>
        public List<Department2> SelectAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Department2>(collectionName);

            var depts = collection.AsQueryable<Department2>().ToList();

            return depts;
        }

        /// <summary>
        /// Update one Department of the mongoDb collection
        /// </summary>
        /// <param name="dept">Department to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Department2 dept)
        {
            if (Delete(dept._id) && Insert(dept))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {dept._id} actualitzat");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en actualitzar el registre de {collectionName} amb id {dept._id}");

                return false;
            }
            Console.ResetColor();

            return true;
        }

    }
}
