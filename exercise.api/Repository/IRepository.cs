using exercise.api.Models;
using System.ComponentModel.DataAnnotations;

namespace exercise.api.Repository
{
    public interface IRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(int id);
        bool AddEmployee(Employee employee);
        bool UpdateEmployee(int id, Employee employee);
        bool DeleteEmployee(int id);
    }
}
