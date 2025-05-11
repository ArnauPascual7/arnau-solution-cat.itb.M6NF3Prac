using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cat.itb.store_PascualArnau.model;

namespace cat.itb.store_PascualArnau
{
    public static class ModelConverterHelper
    {
        public static Employee2 ConvertToEmployee2(Employee emp)
        {
            return new Employee2
            {
                _id = emp._id,
                Surname = emp.Surname,
                Job = emp.Job,
                ManagerId = emp.ManagerId,
                StartDate = emp.StartDate,
                Salary = emp.Salary,
                Commission = emp.Commission,
                Department = emp.Department._id
            };
        }

        public static Employee ConvertToEmployee(Employee2 emp)
        {
            return new Employee
            {
                _id = emp._id,
                Surname = emp.Surname,
                Job = emp.Job,
                ManagerId = emp.ManagerId,
                StartDate = emp.StartDate,
                Salary = emp.Salary,
                Commission = emp.Commission,
                Department = new Department { _id = emp.Department }
            };
        }

        public static Client2 ConvertToClient2(Client client)
        {
            return new Client2
            {
                _id = client._id,
                Name = client.Name,
                Address = client.Address,
                City = client.City,
                St = client.St,
                Zipcode = client.Zipcode,
                Area = client.Area,
                Phone = client.Phone,
                Employee = client.Employee?._id,
                Credit = client.Credit,
                Comments = client.Comments
            };
        }

        public static Client ConvertToClient(Client2 client)
        {
            return new Client
            {
                _id = client._id,
                Name = client.Name,
                Address = client.Address,
                City = client.City,
                St = client.St,
                Zipcode = client.Zipcode,
                Area = client.Area,
                Phone = client.Phone,
                Employee = client.Employee.HasValue ? new Employee { _id = client.Employee.Value } : null,
                Credit = client.Credit,
                Comments = client.Comments
            };
        }

        public static Department2 ConvertToDepartment2(Department department)
        {
            return new Department2
            {
                _id = department._id,
                Name = department.Name,
                Loc = department.Loc
            };
        }

        public static Department ConvertToDepartment(Department2 department)
        {
            return new Department
            {
                _id = department._id,
                Name = department.Name,
                Loc = department.Loc
            };
        }
    }
}
