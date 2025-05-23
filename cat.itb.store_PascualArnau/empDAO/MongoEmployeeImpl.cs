﻿using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public class MongoEmployeeImpl : IEmployeeDAO<Employee2>
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
            var collection = database.GetCollection<Employee2>(collectionName);

            bool correct;

            var deleteFilter = Builders<Employee2>.Filter.Eq("_id", empId);

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
        /// Insert one Employee to the mongoDb collection
        /// </summary>
        /// <param name="emp2">Employee to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Employee2 emp)
        {

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee2>(collectionName);

            bool correct;
            try
            {
                collection.InsertOne(emp);

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
        /// Insert many Employees to the mongoDb collection
        /// </summary>
        /// <param name="emps">Employees to insert</param>
        public void InsertAll(List<Employee2> emps)
        {
            //DeleteAll();

            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee2>(collectionName);

            try
            {
                collection.InsertMany(emps);

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
        /// Select one Employee from mongoDb collection
        /// </summary>
        /// <param name="empId">Id of the Employee</param>
        /// <returns>Selected Employee</returns>
        public Employee2 Select(int empId)
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee2>(collectionName);

            var emp = collection.AsQueryable<Employee2>()
                        .Where(d => d._id == empId)
                        .Single();

            return emp;
        }

        /// <summary>
        /// Select all Employees from mongoDb collection
        /// </summary>
        /// <returns>List of selected Employees</returns>
        public List<Employee2> SelectAll()
        {
            var database = MongoConnection.GetDatabase(dbName);
            var collection = database.GetCollection<Employee2>(collectionName);

            var emps = collection.AsQueryable<Employee2>().ToList();

            return emps;
        }

        /// <summary>
        /// Update one Employee of the mongoDb collection
        /// </summary>
        /// <param name="emp">Employee to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Employee2 emp)
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
