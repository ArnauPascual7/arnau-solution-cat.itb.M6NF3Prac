using cat.itb.store_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.clieDAO
{
    public interface IClientDAO<T>
    {
        void DeleteAll();
        void InsertAll(List<T> clies);
        List<T> SelectAll();
        T Select(int clieId);
        List<T> SelectByEmpId(int empId);
        List<T> SelectByEmpSurname(string empSurname);
        bool Insert(T clie);
        bool Delete(int clieId);
        bool Update(T clie);
    }
}
