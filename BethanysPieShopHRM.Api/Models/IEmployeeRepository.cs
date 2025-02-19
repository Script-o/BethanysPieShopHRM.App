using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.Api.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetLongEmployeeList();
        IEnumerable<Employee> GetTakeLongEmployeeList(int request, int count);
        Employee GetEmployeeById(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
