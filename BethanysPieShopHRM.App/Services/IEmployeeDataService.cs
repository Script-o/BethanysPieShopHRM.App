using BethanysPieShopHRM.Shared;

namespace BethanysPieShopHRM.App.Services
{
    public interface IEmployeeDataService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeDetails(int employeeId);
        Task<IEnumerable<Employee>> GetLongEmployeeList();
        Task<IEnumerable<Employee>> GetTakeLongEmployeeList(int startIndex, int count);
        Task<Employee> AddEmployee(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task DeleteEmployee(int employeeId);
    }
}
