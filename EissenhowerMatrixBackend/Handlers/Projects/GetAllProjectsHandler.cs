using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using Microsoft.EntityFrameworkCore;
using EissenhowerMatrixBackend.Requests.Queries.Projects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.Handlers.Projects
{
    public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
    {
        private readonly TodoDb _db;

        public GetAllProjectsHandler(TodoDb db)
        {
            _db = db;
        }

        public async Task<List<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            return await _db.Projects.ToListAsync(cancellationToken);
        }


    }
}
