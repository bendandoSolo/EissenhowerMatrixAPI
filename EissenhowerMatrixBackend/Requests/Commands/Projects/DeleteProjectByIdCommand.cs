using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Commands.Projects;

public record DeleteProjectByIdCommand(int id) : IRequest<Project?>;