using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class PositionsRepository : IPositionsRepository
{
    private IDapperContext _dapperContext;

    public PositionsRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<DbPosition?> GetPositionByIdAsync(Guid positionId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", department_id as ""DepartmentId"", name as ""Name"" FROM positions
                WHERE id = @id",
            new { id = positionId });

        return await _dapperContext.FirstOrDefault<DbPosition>(queryObject);
    }
    public async Task<List<DbPosition>> GetByDepartmentIdAsync(Guid departmentId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", department_id as ""DepartmentId"", name as ""Name"" FROM positions
            WHERE department_id = @departmentId",
            new
            {
                departmentId
            });

        return await _dapperContext.ListOrEmpty<DbPosition>(queryObject);
    }

    public async Task<DbPosition> AddPositionAsync(DbPosition position)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO positions(department_id, name) VALUES (@DepartmentId, @Name)
                RETURNING id as ""Id"", department_id as ""DepartmentId"", name as ""Name""",
            new
            {
                position.Id, 
                position.DepartmentId, 
                position.Name
            });

        return await _dapperContext.CommandWithResponse<DbPosition>(queryObject);
    }

    public async Task<DbPosition> UpdatePositionAsync(DbPosition position)
    {
        var queryObject = new QueryObject(
            @"UPDATE positions SET department_id = @DepartmentId, name = @Name  
            WHERE id = @id
            RETURNING id as ""Id"", department_id as ""DepartmentId"", name as ""Name""",
            new
            {
                id = position.Id,
                departmentId = position.DepartmentId,
                name = position.Name
            });

        return await _dapperContext.CommandWithResponse<DbPosition>(queryObject);
    }

    public async Task DeletePositionAsync(Guid positionId)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM positions WHERE id = @id",
            new { id = positionId });

        await _dapperContext.Command(queryObject);
    }
}