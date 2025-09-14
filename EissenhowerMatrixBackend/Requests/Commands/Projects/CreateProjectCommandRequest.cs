using EissenhowerMatrixBackend.Models;
using EissenhowerMatrixBackend.Models.ViewModels;
using MediatR;


namespace EissenhowerMatrixBackend.Requests.Commands.Projects;

public record CreateProjectCommandRequest(CreateProjectViewModel createProjectViewModel) : IRequest<Project?>;

