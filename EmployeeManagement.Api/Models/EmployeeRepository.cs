﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = _dbContext.Employees;

            if (!string.IsNullOrEmpty(name))
            {

                query = query.Where(e => e.FirstName.Contains(name)
                                         || e.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
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

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            var result = await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if (result == null) return null;
            _dbContext.Employees.Remove(result);
            await _dbContext.SaveChangesAsync();
            return result;
        }

    }

}