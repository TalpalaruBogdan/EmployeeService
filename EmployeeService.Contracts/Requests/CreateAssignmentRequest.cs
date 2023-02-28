namespace EmployeeService.Contracts.Requests
{
    public class CreateAssignmentRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
