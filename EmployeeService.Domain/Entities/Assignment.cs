namespace EmployeeService.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public Guid ProjectId { get; set; }
        public string Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
