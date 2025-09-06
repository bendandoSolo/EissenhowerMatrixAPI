namespace EissenhowerMatrixBackend.Models.ViewModels;
using EissenhowerMatrixBackend.Constants.Enums;

public class CreateTodoViewModel
    {
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public EissenhowerStatus Priority { get; set; } = EissenhowerStatus.Unassigned;

        public bool ToBuyOrGet { get; set; } = false;
}
