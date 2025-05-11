using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;

namespace cat.itb.store_PascualArnau.depDAO
{
    public class SQLDepartmentImpl : IDepartmentDAO<Department>
    {
        private const string tableName = "clients";

        /// <summary>
        /// Delete one Department from the sql table
        /// </summary>
        /// <param name="deptId">Id of the Department to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int deptId)
        {
            bool correct = false;
            Department dept = Select(deptId);

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(dept);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {deptId} eliminat");

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
                        Console.WriteLine($"Error en eliminar el registre de {tableName} amb id {deptId}");
                    }
                    Console.ResetColor();
                }
            }
            return correct;
        }

        /// <summary>
        /// Delete all Department from the sql table
        /// </summary>
        public void DeleteAll()
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var dept in SelectAll())
                        {
                            session.Delete(dept);
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
        /// Insert one Department to the sql table
        /// </summary>
        /// <param name="dept">Department to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Department dept)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(dept);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre amb Id {dept._id} insertat correctament en {tableName}");

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
                        Console.WriteLine($"Error en insertar el registre amb Id {dept._id} en {tableName}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Insert many Department to the sql table
        /// </summary>
        /// <param name="depts">List of Department to insert</param>
        public void InsertAll(List<Department> depts)
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var dept in depts)
                        {
                            session.Save(dept);
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
        /// Select one Department from the sql table
        /// </summary>
        /// <param name="deptId">Id of the Department to select</param>
        /// <returns>Selected Department</returns>
        public Department Select(int deptId)
        {
            Department dept;
            using (var session = SessionFactorySQLConnection.Open())
            {
                dept = session.Get<Department>(deptId);
            }
            return dept;
        }

        /// <summary>
        /// Select all Client from the sql table
        /// </summary>
        /// <returns>List of the Selected Client</returns>
        public List<Department> SelectAll()
        {
            List<Department> depts;
            using (var session = SessionFactorySQLConnection.Open())
            {
                depts = session.Query<Department>().Select(d => d).ToList();
            }
            return depts;
        }

        /// <summary>
        /// Update one Department from the sql table
        /// </summary>
        /// <param name="dept">Department to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Department dept)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(dept);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {dept._id} actualitzat correctament");

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
                        Console.WriteLine($"Error en actualitzar el registre de {tableName} amb id {dept._id}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }
    }
}
