using cat.itb.store_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.clieDAO
{
    public interface IClientDAO
    {
        void DeleteAll();
        void InsertAll(List<Client> clies);
        List<Client> SelectAll();
        Client Select(int clieId);
        bool Insert(Client clie);
        bool Delete(int clieId);
        bool Update(Client clie);
    }
}
