﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApplication.Employees
{
    public class EmployeeUpdateDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int? ReportsTo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}