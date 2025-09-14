namespace EissenhowerMatrixBackend.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? CompletionDate { get; set; } = null;
    public List<Todo> ProjectTodos { get; set; } = [];
    public DateTime? Deleted { get; set; } = null;

   /// public int ProjectPriority { get; set; } = 0; // 0 = unassigned, 1 = low, 2 = medium, 3 = high
}