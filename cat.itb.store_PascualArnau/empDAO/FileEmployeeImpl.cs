using cat.itb.store_PascualArnau.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public class FileEmployeeImpl : IEmployeeDAO
    {
        private const string fileName = "employees.json";
        private const string filePath = @"..\..\..\files\" + fileName;

        public bool Delete(int empId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Employee emp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert Employees into the file
        /// </summary>
        /// <param name="emps">Employees to insert</param>
        public void InsertAll(List<Employee> emps1)
        {
            Debug.WriteLine("?: Employees Json File Path -> " + Path.GetFullPath(filePath));

            List<Employee2> emps = new List<Employee2>();

            foreach (var e in emps1)
            {
                emps.Add(new Employee2()
                {
                    _id = e._id,
                    Surname = e.Surname,
                    Job = e.Job,
                    ManagerId = e.ManagerId,
                    StartDate = e.StartDate,
                    Salary = e.Salary,
                    Commission = e.Commission,
                    Department = e.Department._id
                });
            }

            FileInfo file = new FileInfo(filePath);

            using (StreamWriter writer = file.CreateText())
            {
                try
                {
                    foreach (var emp in emps)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(emp));
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nRegistres insertats correctament en {fileName}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"?: Error inserting registers in {fileName} -> {ex}");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nError en insertar els registres de {fileName}");
                }
            }
            Console.ResetColor();
        }

        public Employee Select(int empId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all Employees from the file
        /// </summary>
        /// <returns>Employees in the file</returns>
        public List<Employee> SelectAll()
        {
            FileInfo file = new FileInfo(filePath);
            List<Employee> list = new List<Employee>();

            using (StreamReader writer = file.OpenText())
            {
                string? emp;

                while ((emp = writer.ReadLine()) != null)
                {
                    list.Add(JsonConvert.DeserializeObject<Employee>(emp));
                }
            }
            return list;
        }

        public bool Update(Employee emp)
        {
            throw new NotImplementedException();
        }
    }
}
