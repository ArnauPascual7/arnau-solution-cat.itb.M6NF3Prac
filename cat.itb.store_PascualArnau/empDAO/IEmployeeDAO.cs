using cat.itb.store_PascualArnau.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.empDAO
{
    public interface IEmployeeDAO
    {
        void DeleteAll();
        void InsertAll(List<Employee> emps);
        List<Employee> SelectAll();
        Employee Select(int empId);
        bool Insert(Employee emp);
        bool Delete(int empId);
        bool Update(Employee emp);
    }
}
