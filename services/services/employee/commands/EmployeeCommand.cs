using core.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.employee.commands
{
    public class EmployeeCommand : Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
