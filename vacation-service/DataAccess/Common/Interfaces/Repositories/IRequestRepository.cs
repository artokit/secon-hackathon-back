using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories
{
    public interface IRequestRepository
    {
        public Task<DbRequest> CreateRequestAsync(DbRequest request);
        public Task<DbRequest?> GetRequestByIdAsync(Guid id);
        public Task<DbRequest> UpdateRequestAsync(Guid id, DbRequest request);
        public Task DeleteRequestAsync(Guid id);
    }
}
