using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Contracts.Responses
{
    public class AssignmentResponse
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
