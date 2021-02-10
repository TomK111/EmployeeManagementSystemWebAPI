using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _db;
        public EmployeeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateEmployee(Employee employee)
        {
            _db.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(Employee employee)
        {
            _db.Remove(employee);
            return Save();
        }

        public bool EmployeeExists(string name)
        {
            bool value = _db.Employees.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool EmployeeExists(int id)
        {
            return _db.Employees.Any(a => a.Id == id);
        }

        public Employee GetEmployee(int id)
        {
            return _db.Employees.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<Employee> GetEmployees()
        {
            return _db.Employees.OrderBy(a => a.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool UpdateEmployee(Employee employee)
        {
            _db.Employees.Update(employee);
            return Save();
        }
    }
}
