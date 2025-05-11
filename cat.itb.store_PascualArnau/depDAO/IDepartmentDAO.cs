using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.store_PascualArnau.depDAO
{
    public interface IDepartmentDAO<T>
    {
        void DeleteAll();
        void InsertAll(List<T> depts);
        List<T> SelectAll();
        T Select(int deptId);
        bool Insert(T dept);
        bool Delete(int deptId);
        bool Update(T dept);
    }
}
