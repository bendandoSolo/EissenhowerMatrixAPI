using EissenhowerMatrixBackend.DataBaseConnection;
using EissenhowerMatrixBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace EissenhowerMatrixBackend.Handlers.Projects
{
    public class GetAllProjectsQueryHandler
    {
        private readonly TodoDb _db;

        public GetAllProjectsQueryHandler(TodoDb db)
        {
            _db = db;
        }

        public async Task<List<Project>> Handle(GetAllProjectsQueryHandler request, CancellationToken cancellationToken)
        {
            return await _db.Projects.ToListAsync(cancellationToken);
        }


    }
}
