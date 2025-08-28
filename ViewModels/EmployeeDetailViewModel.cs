using EntrepriseEmploye.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EntrepriseEmploye.ViewModels
{
    public class EmployeeDetailViewModel
    {
        public Employee SelectedEmployee { get; set; }

        public EmployeeDetailViewModel(Employee employee)
        {
            SelectedEmployee = employee;
        }
    }
}
