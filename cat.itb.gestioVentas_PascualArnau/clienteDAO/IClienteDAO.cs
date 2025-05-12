using cat.itb.gestioVentas_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.gestioVentas_PascualArnau.clienteDAO
{
    public interface IClienteDAO
    {
        void DeleteAll();
        void InsertAll(List<Cliente> clies);
        List<Cliente> SelectAll();
        Cliente Select(int clieId);
        Cliente SelectByName(string clieName);
        bool Insert(Cliente clie);
        bool Delete(int clieId);
        bool Update(Cliente clie);
    }
}
