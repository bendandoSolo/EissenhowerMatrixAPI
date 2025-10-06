using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Requests.Commands.Projects;
using MediatR;

namespace EissenhowerMatrixBackend.Handlers.Projects;

public class DeleteProjectByIdCommandHandler : IRequestHandler<DeleteProjectByIdCommand, Project?>
{
    private readonly TodoDb _db;

    public DeleteProjectByIdCommandHandler(TodoDb db)
    {
        _db = db;
    }

    public async Task<Project?> Handle(DeleteProjectByIdCommand request, CancellationToken cancellationToken)
    {
        if (await _db.Projects.FindAsync(request.id, cancellationToken) is not Project project)
            return null;

        // Soft delete (mark Deleted timestamp). If already deleted, treat as not found.
        if (project.Deleted is not null)
            return null;

        project.Deleted = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);
        return project;
    }
}