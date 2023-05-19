namespace EissenhowerMatrixBackend.DataBaseConnection.Models;

public class Todo
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? CompletionDate { get; set; }
    public EissenhowerStatus Priority { get; set; } = EissenhowerStatus.Unassigned;
}

// can use description tags on enums and custom GetDescription method to get the description to include spaces in description
public enum EissenhowerStatus
{
    Unassigned,
    UrgentPriority,
    NotUrgentPriority,
    UrgentLowPriority,
    NotUrgentLowPriority,
}