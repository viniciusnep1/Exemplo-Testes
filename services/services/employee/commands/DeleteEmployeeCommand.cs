﻿using core.commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace services.services.employee.commands
{
    public class DeleteEmployeeCommand : Command
    {
        public Guid Id { get; set; }
    }
}
