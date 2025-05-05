using cat.itb.store_PascualArnau.connections;
using cat.itb.store_PascualArnau.model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public class SQLEmployeeImpl : EmployeeDAO
    {
        public const string tableName = "employees";

        /// <summary>
        /// Delete one Employee from the sql table
        /// </summary>
        /// <param name="empId">Id of the Employee to delete</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Delete(int empId)
        {
            bool correct = false;
            Employee emp = Select(empId);

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(emp);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {empId} eliminat");

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
                        Console.WriteLine($"Error en eliminar el registre de {tableName} amb id {empId}");
                    }
                    Console.ResetColor();
                }
            }
            return correct;
        }

        /// <summary>
        /// Delete all Employees from the sql table
        /// </summary>
        public void DeleteAll()
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach(var emp in SelectAll())
                        {
                            session.Delete(emp);
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
        /// Insert one Employee to the sql table
        /// </summary>
        /// <param name="emp">Employee to insert</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Insert(Employee emp)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(emp);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre amb Id {emp._id} insertat correctament en {tableName}");

                        correct = true;
                    }
                    catch
                    {
                        if (!tx.WasCommitted)
                        {
                            tx.Rollback();
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error en insertar el registre amb Id {emp._id} en {tableName}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }

        /// <summary>
        /// Insert many Employees to the sql table
        /// </summary>
        /// <param name="emps"></param>
        public void InsertAll(List<Employee> emps)
        {
            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        foreach (var emp in emps)
                        {
                            session.Save(emp);
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
        /// Select one Employee from the sql table
        /// </summary>
        /// <param name="empId">Id of the Employee to select</param>
        /// <returns>Selected Employee</returns>
        public Employee Select(int empId)
        {
            Employee emp;
            using (var session = SessionFactorySQLConnection.Open())
            {
                emp = session.Get<Employee>(empId);
            }
            return emp;
        }

        /// <summary>
        /// Select all Employees from the sql table
        /// </summary>
        /// <returns>List of the Selected Employees</returns>
        public List<Employee> SelectAll()
        {
            List<Employee> emps;
            using (var session = SessionFactorySQLConnection.Open())
            {
                //emps = (from e in session.Query<Employee>() select e).ToList();
                emps = session.Query<Employee>().Select(e => e).ToList();
            }
            return emps;

        }

        /// <summary>
        /// Update one Employee from the sql table
        /// </summary>
        /// <param name="emp">Employee to update</param>
        /// <returns>If operation was Succesfull</returns>
        public bool Update(Employee emp)
        {
            bool correct = false;

            using (var session = SessionFactorySQLConnection.Open())
            {
                using (var tx = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(emp);
                        tx.Commit();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Registre de {tableName} amb id {emp._id} actualitzat correctament");

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
                        Console.WriteLine($"Error en actualitzar el registre de {tableName} amb id {emp._id}");
                    }
                }
            }
            Console.ResetColor();

            return correct;
        }
    }
}
