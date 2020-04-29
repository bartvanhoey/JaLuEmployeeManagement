using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.API.Models
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int departmentId);
    }
}