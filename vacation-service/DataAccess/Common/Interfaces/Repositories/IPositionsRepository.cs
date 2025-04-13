using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories;

public interface IPositionsRepository
{
    public Task<DbPosition?> GetPositionByIdAsync(Guid positionId);
    public Task<List<DbPosition>> GetByDepartmentIdAsync(Guid id);
    public Task<DbPosition> AddPositionAsync(DbPosition position);
    public Task<DbPosition> UpdatePositionAsync(DbPosition position);
    public Task DeletePositionAsync(Guid id);
}