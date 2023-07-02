using MediatR.NotificationPublishers;

namespace EissenhowerMatrixBackend.DataBaseConnection.Models;

public class Todo
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    public DateTime? CompletionDate { get; set; }
    public EissenhowerStatus Priority { get; set; } = EissenhowerStatus.Unassigned;

    public bool ToBuyOrGet { get; set; } = false;
    
    // TODO: add a list of subtasks, and a list of follow up tasks,
    // add reoccuring todo's automatically recreated after time period. 
    //follow up tasks, such as when completed check your account in one week etc?

    //perhaps todo's that increase in priority
    //after a certain amount of time they become a priority, if ignored for say a week or month etc

    //add a time estimation
    //add a difficultly estimation
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