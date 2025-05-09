using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.clieDAO
{
    public class SQLClientImpl : IClientDAO
    {
        private const string tableName = "clients";

        /// <summary>
        /// Delete one Client from the sql table
        /// </summary>
        /// <param name="clieId">Id of the Client to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int clieId)
        {
            bool correct = false;
            Client clie = Select(clieId);

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(clie);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {clieId} eliminat");

                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        Debug.WriteLine($"?: Error deleting in {tableName} -> {ex}");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en eliminar el registre de {tableName} amb id {clieId}");
                    }
                    Console.ResetColor();
                }
            }
            return correct;
        }

        /// <summary>
        /// Delete all Client from the sql table
        /// </summary>
        public void DeleteAll()
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var clie in SelectAll())
                        {
                            session.Delete(clie);
                        }
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Tots els registres de {tableName} eliminats");
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        Debug.WriteLine($"?: Error deleting in {tableName} -> {ex}");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en eliminar els registres de {tableName}");
                    }
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Insert one Client to the sql table
        /// </summary>
        /// <param name="clie">Client to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Client clie)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(clie);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre amb Id {clie._id} insertat correctament en {tableName}");

                        correct = true;
                    }
                    catch
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en insertar el registre amb Id {clie._id} en {tableName}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Insert many Client to the sql table
        /// </summary>
        /// <param name="clies">List of Clients to insert</param>
        public void InsertAll(List<Client> clies)
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var clie in clies)
                        {
                            session.Save(clie);
                        }
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registres insertats correctament en {tableName}");
                    }
                    catch
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en insertar els registres de {tableName}");
                    }
                }
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Select one Client from the sql table
        /// </summary>
        /// <param name="clieId">Id of the Client to select</param>
        /// <returns>Selected Client</returns>
        public Client Select(int clieId)
        {
            Client clie;
            using (var session = SessionFactorySQLConnection.Open())
            {
                clie = session.Get<Client>(clieId);
            }
            return clie;
        }

        /// <summary>
        /// Select all Client from the sql table
        /// </summary>
        /// <returns>List of the Selected Client</returns>
        public List<Client> SelectAll()
        {
            List<Client> clies;
            using (var session = SessionFactorySQLConnection.Open())
            {
                clies = session.Query<Client>().Select(e => e).ToList();
            }
            return clies;

        }

        /// <summary>
        /// Update one Client from the sql table
        /// </summary>
        /// <param name="clie">Client to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Client clie)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(clie);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {clie._id} actualitzat correctament");

                        correct = true;
                    }
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        Debug.WriteLine($"?: Error updating in {tableName} -> {ex}");

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en actualitzar el registre de {tableName} amb id {clie._id}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }
    }
}
