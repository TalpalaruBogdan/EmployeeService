using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Responses
{
    public class EmployeesResponse
    {
        public IEnumerable<EmployeeResponse> Items { get; set; }
    }
}
