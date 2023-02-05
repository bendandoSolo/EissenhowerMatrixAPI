namespace EissenhowerMatrixBackend.DataBaseConnection.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public DateTime? CompletionDate { get; set; }

    }
}