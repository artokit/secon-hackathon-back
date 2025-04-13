using DataAccess.Common.Interfaces;
using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IDapperContext _dapperContext;

        public OrdersRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<DbOrder> CreateOrderAsync(DbOrder order)
        {
            var queryObject = new QueryObject(@"
            INSERT INTO orders (creator_id, departament_id, created_data, status) 
            VALUES (@CreatorId, @DepartamentId, @CreatedData, @Status)
            RETURNING id as ""Id"", creator_id as ""Creator_Id"", departament_id as ""Departament_Id"", created_data as ""Created_Data"", status as ""Status""",
                new
                {
                    CreatorId = order.Creator_Id,
                    DepartamentId = order.Departament_Id,
                    CreatedData = order.Created_Data,
                    Status = order.Status
                });

            return await _dapperContext.CommandWithResponse<DbOrder>(queryObject);
        }

        public async Task DeleteOrderAsync(Guid id)
        {
            var queryObject = new QueryObject(
                @"DELETE FROM orders WHERE id = @Id",
                new { Id = id });

            await _dapperContext.Command(queryObject);
        }

        public async Task<DbOrder?> GetOrderByIdAsync(Guid id)
        {
            var queryObject = new QueryObject(@"
            SELECT id as ""Id"", creator_id as ""Creator_Id"", departament_id as ""Departament_Id"", created_data as ""Created_Data"", status as ""Status"" 
            FROM orders WHERE id = @Id",
                new { Id = id });

            return await _dapperContext.FirstOrDefault<DbOrder>(queryObject);
        }

        public async Task<DbOrder> UpdateOrderAsync(Guid id, DbOrder order)
        {
            var queryObject = new QueryObject(@"
            UPDATE orders SET creator_id = @CreatorId, departament_id = @DepartamentId, created_data = @CreatedData, status = @Status 
            WHERE id = @Id
            RETURNING id as ""Id"", creator_id as ""Creator_Id"", departament_id as ""Departament_Id"", created_data as ""Created_Data"", status as ""Status""",
                new
                {
                    Id = id,
                    CreatorId = order.Creator_Id,
                    DepartamentId = order.Departament_Id,
                    CreatedData = order.Created_Data,
                    Status = order.Status
                });

            return await _dapperContext.CommandWithResponse<DbOrder>(queryObject);
        }
    }

}
