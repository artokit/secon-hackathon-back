using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class RequestsRepository : IRequestRepository
    {
        private readonly IDapperContext _dapperContext;

        public RequestsRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbRequest> CreateRequestAsync(DbRequest request)
        {
            var queryObject = new QueryObject(@"
                INSERT INTO requests (user_id, start_date, end_date, extensions_days, fact_date, reason_id, comment)
                VALUES (@UserId, @StartDate, @EndDate, @ExtensionsDays, @FactDate, @ReasonId, @Comment)
                RETURNING id as ""Id"", user_id as ""User_Id"", start_date as ""Start_Date"", end_date as ""End_Date"", extensions_days as ""Extensions_Days"", fact_date as ""Fact_Date"", reason_id as ""Reason_Id"", comment as ""Comment""",
                new
                {
                    UserId = request.User_Id,
                    StartDate = request.Start_Date,
                    EndDate = request.End_Date,
                    ExtensionsDays = request.Extensions_Days,
                    FactDate = request.Fact_Date,
                    ReasonId = request.Reason_Id,
                    Comment = request.Comment
                });

            return await _dapperContext.CommandWithResponse<DbRequest>(queryObject);
        }

        public async Task DeleteRequestAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"DELETE FROM requests WHERE id = @Id",
                new { Id = id });

            await _dapperContext.Command(queryObject);
        }

        public async Task<DbRequest?> GetRequestByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(@"
                SELECT id as ""Id"", user_id as ""User_Id"", start_date as ""Start_Date"", end_date as ""End_Date"", extensions_days as ""Extensions_Days"", fact_date as ""Fact_Date"", reason_id as ""Reason_Id"", comment as ""Comment""
                FROM requests
                WHERE id = @Id",
                new { Id = id });

            return await _dapperContext.FirstOrDefault<DbRequest>(queryObject);
        }

        public async Task<DbRequest> UpdateRequestAsync(Guid id, DbRequest request)
        {
            var queryObject = new QueryObject(@"
                UPDATE requests
                SET user_id = @UserId,
                    start_date = @StartDate,
                    end_date = @EndDate,
                    extensions_days = @ExtensionsDays,
                    fact_date = @FactDate,
                    reason_id = @ReasonId,
                    comment = @Comment
                WHERE id = @Id
                RETURNING id as ""Id"", user_id as ""User_Id"", start_date as ""Start_Date"", end_date as ""End_Date"", extensions_days as ""Extensions_Days"", fact_date as ""Fact_Date"", reason_id as ""Reason_Id"", comment as ""Comment""",
                new
                {
                    Id = id,
                    UserId = request.User_Id,
                    StartDate = request.Start_Date,
                    EndDate = request.End_Date,
                    ExtensionsDays = request.Extensions_Days,
                    FactDate = request.Fact_Date,
                    ReasonId = request.Reason_Id,
                    Comment = request.Comment
                });

            return await _dapperContext.CommandWithResponse<DbRequest>(queryObject);
        }
    }
}
