using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cat.itb.store_PascualArnau.clieDAO
{
    public class MongoClientImpl : IClientDAO<Client2>
    {
        private const string dbName = "itb";
        private const string collectionName = "clients";

        /// <summary>
        /// Delete an Client from the mongoDb collection
        /// </summary>
        /// <param name="clieId">Id of the Client to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int clieId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

            bool correct;

            var deleteFilter = Builders<Client2>.Filter.Eq("_id", clieId);

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
        public bool Insert(Client2 clie)
        {

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

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
        public void InsertAll(List<Client2> clies)
        {
            //DeleteAll();

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

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
        public Client2 Select(int clieId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

            var clie = collection.AsQueryable<Client2>()
                        .Where(d => d._id == clieId)
                        .Single();

            return clie;
        }

        /// <summary>
        /// Select all Employees from mongoDb collection
        /// </summary>
        /// <returns>List of selected Employees</returns>
        public List<Client2> SelectAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

            var clies = collection.AsQueryable<Client2>().ToList();

            return clies;
        }

        public List<Client2> SelectByEmpId(int empId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

            var clies = collection.AsQueryable<Client2>().Where(d => d.Employee == empId).ToList();

            return clies;
        }

        public List<Client2> SelectByEmpSurname(string empSurname)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Client2>(collectionName);

            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup",
                    new BsonDocument
                    {
                        { "from", "employees" },
                        { "localField", "Employee" },
                        { "foreignField", "_id" },
                        { "as", "Employee" }
                    }),
                new BsonDocument("$unwind", "$Employee"),
                new BsonDocument("$match",
                    new BsonDocument("Employee.Surname", empSurname))
            };

            var result = collection.Aggregate<BsonDocument>(pipeline).ToList();
            
            List<Client2> clies = new List<Client2>();

            foreach (var item in result)
            {
                var clie = new Client2
                {
                    _id = item["_id"].AsInt32,
                    Name = item["Name"].AsString,
                    Address = item["Address"].AsString,
                    City = item["City"].AsString,
                    St = item["St"].AsString,
                    Zipcode = item["Zipcode"].AsString,
                    Area = item["Area"].AsInt32,
                    Phone = item["Phone"].AsString,
                    Employee = item["Employee"]["_id"].AsInt32,
                    Credit = (float)item["Credit"].AsDouble,
                    Comments = item["Comments"].AsString
                };
                clies.Add(clie);
            }

            return clies;
        }

        /// <summary>
        /// Update one Client of the mongoDb collection
        /// </summary>
        /// <param name="clie">Client to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Client2 clie)
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
