using cat.itb.gestioVentas_PascualArnau.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.clienteDAO
{
    public class FileClienteImpl : IClienteDAO
    {
        private const string fileName = "cliente.json";
        private const string filePath = @"..\..\..\files\" + fileName;

        public bool Delete(int clieId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(Cliente clie)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert Client into the file
        /// </summary>
        /// <param name="emps">Client to insert</param>
        public void InsertAll(List<Cliente> clies)
        {
            Debug.WriteLine("?: Client Json File Path -> " + Path.GetFullPath(filePath));

            FileInfo file = new FileInfo(filePath);

            using (StreamWriter writer = file.CreateText())
            {
                try
                {
                    foreach (var clie in clies)
                    {
                        writer.WriteLine(JsonConvert.SerializeObject(clie));
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

        public Cliente Select(int clieId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Select all Client from the file
        /// </summary>
        /// <returns>Client in the file</returns>
        public List<Cliente> SelectAll()
        {
            FileInfo file = new FileInfo(filePath);
            List<Cliente> list = new List<Cliente>();

            using (StreamReader writer = file.OpenText())
            {
                string? clie;

                while ((clie = writer.ReadLine()) != null)
                {
                    list.Add(JsonConvert.DeserializeObject<Cliente>(clie));
                }
            }
            return list;
        }

        public Cliente SelectByName(string clieName)
        {
            throw new NotImplementedException();
        }

        public bool Update(Cliente clie)
        {
            throw new NotImplementedException();
        }

    }
}
