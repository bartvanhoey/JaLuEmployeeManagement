using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var employeeFound = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);

            if (employeeFound != null)
            {
                employeeFound.FirstName = employee.FirstName;
                employeeFound.LastName = employee.LastName;
                employeeFound.Email = employee.Email;
                employeeFound.DateOfBirth = employee.DateOfBirth;
                employeeFound.Gender = employee.Gender;
                employeeFound.DepartmentId = employee.DepartmentId;
                employeeFound.PhotoPath = employee.PhotoPath;
                await _dbContext.SaveChangesAsync();
                return employeeFound;
            }
            return null;
        }

        public async void DeleteEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (employee == null) return;
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}