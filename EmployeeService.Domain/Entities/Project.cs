namespace EmployeeService.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
    }
}
