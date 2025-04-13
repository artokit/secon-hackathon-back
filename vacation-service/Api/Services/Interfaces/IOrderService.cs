using Api.Dto.Orders.Requests;
using Api.Dto.Orders.Responses;

namespace Api.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<GetOrderResponseDto> CreateAsync(CreateOrderRequestDto createOrderRequestDto);
        public Task<GetOrderResponseDto> GetByIdAsync(Guid orderId);
        public Task<GetOrderResponseDto> UpdateAsync(Guid orderId, UpdateOrderRequestDto updateOrderRequestDto);
        public Task DeleteAsync(Guid orderId);
    }
}
