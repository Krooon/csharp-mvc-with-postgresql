using exercise.api.Data;
using exercise.api.Models;


namespace exercise.api.Repository
{
    public class Repository : IRepository
    {
        public IEnumerable<Employee> GetEmployees()
        {
            using (var db = new DataContext())
            {
                return db.Employees.ToList();
            }
        }

        public Employee GetEmployee(int id)
        {
            Employee? employee = null;

            using (var db = new DataContext())
            {
                employee = db.Employees.FirstOrDefault(e => e.id == id);
            }

            return employee;
        }

        public bool AddEmployee(Employee employee)
        {
            using (var db = new DataContext())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateEmployee(int id, Employee employee)
        {
            using (var db = new DataContext())
            {
                var target = db.Employees.FirstOrDefault(e => e.id == id);
                if (target != null)
                {
                    db.Employees.Attach(target);
                    target.name = employee.name;
                    target.jobName = employee.jobName;
                    target.salaryGrade = employee.salaryGrade;
                    target.department = employee.department;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeleteEmployee(int id)
        {
            using (var db = new DataContext())
            {
                var target = db.Employees.FirstOrDefault(e => e.id == id);
                if (target != null)
                {
                    db.Remove(target);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}



       
