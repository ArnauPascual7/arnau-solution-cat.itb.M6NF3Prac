using cat.itb.gestioVentas_PascualArnau.connections;
using cat.itb.gestioVentas_PascualArnau.model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.clienteDAO
{
    public class MongoClienteImpl : IClienteDAO
    {
        private const string dbName = "itb";
        private const string collectionName = "cliente";

        /// <summary>
        /// Delete an Client from the mongoDb collection
        /// </summary>
        /// <param name="clieId">Id of the Client to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int clieId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            bool correct;

            var deleteFilter = Builders<Cliente>.Filter.Eq("_id", clieId);

            var depDeleted = collection.DeleteOne(deleteFilter);

            if (depDeleted.DeletedCount != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {clieId} eliminat");

                correct = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en eliminar el registre de {collectionName} amb id {clieId}");

                correct = false;
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Delete all Client from mongoDb collection
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
        /// Insert one Client to the mongoDb collection
        /// </summary>
        /// <param name="clie">Client to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Cliente clie)
        {

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            bool correct;
            try
            {
                collection.InsertOne(clie);

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
        /// Insert many Client to the mongoDb collection
        /// </summary>
        /// <param name="clies">Client to insert</param>
        public void InsertAll(List<Cliente> clies)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            try
            {
                collection.InsertMany(clies);

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
        /// Select one Client from mongoDb collection
        /// </summary>
        /// <param name="clieId">Id of the Client</param>
        /// <returns>Selected Client</returns>
        public Cliente Select(int clieId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            var clie = collection.AsQueryable<Cliente>()
                        .Where(c => c._id == clieId)
                        .Single();

            return clie;
        }

        /// <summary>
        /// Select all Employees from mongoDb collection
        /// </summary>
        /// <returns>List of selected Employees</returns>
        public List<Cliente> SelectAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            var clies = collection.AsQueryable<Cliente>().ToList();
            return clies;
        }

        public Cliente SelectByName(string clieName)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Cliente>(collectionName);

            var clie = collection.AsQueryable<Cliente>()
                        .Where(c => c.Nombre == clieName)
                        .Single();

            return clie;

        }

        /// <summary>
        /// Update one Client of the mongoDb collection
        /// </summary>
        /// <param name="clie">Client to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Cliente clie)
        {
            if (Delete(clie._id) && Insert(clie))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Registre de {collectionName} amb id {clie._id} actualitzat");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error en actualitzar el registre de {collectionName} amb id {clie._id}");

                return false;
            }
            Console.ResetColor();

            return true;
        }
    }
}
