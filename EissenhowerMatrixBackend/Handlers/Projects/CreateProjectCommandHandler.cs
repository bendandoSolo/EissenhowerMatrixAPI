using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Requests.Commands.Projects;
using MediatR;
using EissenhowerMatrixBackend.Models.ViewModels;



namespace EissenhowerMatrixBackend.Handlers.Projects;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommandRequest, Project?>
{
    private readonly TodoDb _db;

    public CreateProjectCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Project?> Handle(CreateProjectCommandRequest request, CancellationToken cancellationToken)
    {
        CreateProjectViewModel createProjectRequest = request.createProjectViewModel;

        Project newProject = new Project
        {
            Name = createProjectRequest.Name,
            Description = createProjectRequest.Description,
        };

        _db.Projects.Add(newProject);

        await _db.SaveChangesAsync();

        return newProject;
    }
}

