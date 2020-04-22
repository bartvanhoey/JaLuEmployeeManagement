using System.Collections.Generic;
using System.Linq;
using EmployeeManagement.Models;

namespace EmployeeManagement.Api.Models
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext;

        public DepartmentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Department> GetDepartments()
        {
            return _dbContext.Departments;
        }

        public Department GetDepartment(int departmentId)
        {
            return _dbContext.Departments
                .FirstOrDefault(d => d.DepartmentId == departmentId);
        }
    }
}