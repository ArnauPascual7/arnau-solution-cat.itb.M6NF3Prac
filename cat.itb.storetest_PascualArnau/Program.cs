using cat.itb.store_PascualArnau.model;
using StoreLib = cat.itb.store_PascualArnau;
namespace cat.itb.storetest_PascualArnau
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //StoreLib.InitializeDataHelper.InitializeData();

            StoreLib.empDAO.SQLEmployeeImpl sqlEmployee = new StoreLib.empDAO.SQLEmployeeImpl();

            sqlEmployee.Insert(new StoreLib.model.Employee
            {
                _id = 33,
                Surname = "Markitus98",
                Job = "FIFA",
                ManagerId = null,
                StartDate = DateTime.Now,
                Salary = 0,
                Commission = null,
                Department = new Department { _id = 10 }
            });
        }
    }
}
