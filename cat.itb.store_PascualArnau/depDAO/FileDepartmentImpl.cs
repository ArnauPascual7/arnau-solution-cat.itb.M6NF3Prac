using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.store_PascualArnau.model;
using Newtonsoft.Json;

namespace cat.itb.store_PascualArnau.depDAO
{
    public class FileDepartmentImpl : IDepartmentDAO<Department>
    {
        private const string fileName = "departments.json";
        private const string filePath = @"..\..\..\files\" + fileName;

        public bool Delete(int deptId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Department dept)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert Department into the file
        /// </summary>
        /// <param name="depts">Department to insert</param>
        public void InsertAll(List<Department> depts)
        {
            Debug.WriteLine("?: Client Json File Path -> " + Path.GetFullPath(filePath));

            List<Department2> depts2 = new List<Department2>();

            foreach (var d in depts)
            {
                depts2.Add(ModelConverterHelper.ConvertToDepartment2(d));
            }

            FileInfo file = new FileInfo(filePath);

            using (StreamWriter writer = file.CreateText())
            {
                try
                {
                    foreach (var dept in depts2)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(dept));
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

        public Department Select(int deptId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all Department from the file
        /// </summary>
        /// <returns>Department in the file</returns>
        public List<Department> SelectAll()
        {
            FileInfo file = new FileInfo(filePath);
            List<Department> list = new List<Department>();

            using (StreamReader writer = file.OpenText())
            {
                string? dept;

                while ((dept = writer.ReadLine()) != null)
                {
                    list.Add(ModelConverterHelper.ConvertToDepartment(JsonConvert.DeserializeObject<Department2>(dept)));
                }
            }
            return list;
        }

        public bool Update(Department dept)
        {
            throw new NotImplementedException();
        }
    }
}
