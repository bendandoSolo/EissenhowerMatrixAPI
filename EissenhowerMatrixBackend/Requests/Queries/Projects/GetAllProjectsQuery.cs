using EissenhowerMatrixBackend.Models;
using MediatR;

namespace EissenhowerMatrixBackend.Requests.Queries.Projects;

    public record GetAllProjectsQuery : IRequest<List<Project>>;
