﻿using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Web.Services;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeListBase : ComponentBase
    {

        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }


    }
}
