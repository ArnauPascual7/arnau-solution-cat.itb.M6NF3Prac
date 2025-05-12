using cat.itb.gestioVentas_PascualArnau.connections;
using cat.itb.gestioVentas_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.clienteDAO
{
    public class SQLClienteImpl : IClienteDAO
    {
        private const string tableName = "cliente";

        /// <summary>
        /// Delete one Client from the sql table
        /// </summary>
        /// <param name="clieId">Id of the Client to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int clieId)
        {
            bool correct = false;
            Cliente clie = Select(clieId);

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
        public bool Insert(Cliente clie)
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
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        Debug.WriteLine($"?: Error inserting in {tableName} -> {ex}");

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
        public void InsertAll(List<Cliente> clies)
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
                    catch (Exception ex)
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }
                        Debug.WriteLine($"?: Error inserting list in {tableName} -> {ex}");

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
        public Cliente Select(int clieId)
        {
            Cliente clie;
            using (var session = SessionFactorySQLConnection.Open())
            {
                clie = session.Get<Cliente>(clieId);
            }
            return clie;
        }

        /// <summary>
        /// Select all Client from the sql table
        /// </summary>
        /// <returns>List of the Selected Client</returns>
        public List<Cliente> SelectAll()
        {
            List<Cliente> clies;
            using (var session = SessionFactorySQLConnection.Open())
            {
                clies = session.Query<Cliente>().ToList();
            }
            return clies;
        }

        public Cliente SelectByName(string clieName)
        {
            Cliente clie;
            using (var session = SessionFactorySQLConnection.Open())
            {
                clie = session.Query<Cliente>().Where(c => c.Nombre == clieName).Single();
            }
            return clie;
        }

        /// <summary>
        /// Update one Client from the sql table
        /// </summary>
        /// <param name="clie">Client to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Cliente clie)
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
