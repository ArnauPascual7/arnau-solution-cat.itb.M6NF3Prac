using cat.itb.store_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public interface IEmployeeDAO<T>
    {
        void DeleteAll();
        void InsertAll(List<T> emps);
        List<T> SelectAll();
        T Select(int empId);
        bool Insert(T emp);
        bool Delete(int empId);
        bool Update(T emp);
    }
}
