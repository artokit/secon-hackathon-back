using DataAccess.Models;

namespace DataAccess.Common.Interfaces.Repositories
{
    public interface IOrdersRepository
    {
        public Task<DbOrder> CreateOrderAsync(DbOrder order);
        public Task<DbOrder?> GetOrderByIdAsync(Guid id);
        public Task<DbOrder> UpdateOrderAsync(Guid id, DbOrder order);
        public Task DeleteOrderAsync(Guid id);
    }
}
