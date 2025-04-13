using Api.Dto.Authorization.Requests;
using Api.Dto.Orders.Requests;
using Api.Dto.Orders.Responses;
using Api.Exceptions.Departments;
using Api.Exceptions.Orders;
using Api.Services.Interfaces;
using Api.Mappers;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Repositories;
using Api.Exceptions.Users;

namespace Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<GetOrderResponseDto> CreateAsync(CreateOrderRequestDto createOrderRequestDto)
        {
            if (createOrderRequestDto == null)
                throw new ArgumentNullException(nameof(CreateOrderRequestDto));

            var dbOrder = createOrderRequestDto.MapToDb();
            dbOrder.Created_Data = DateTime.UtcNow;

            var createdOrder = await _ordersRepository.CreateOrderAsync(dbOrder);
            return createdOrder.MapToDto();
        }

        public async Task<GetOrderResponseDto> GetByIdAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new OrderArgumentException();

            var order = await _ordersRepository.GetOrderByIdAsync(orderId);

            if (order is null)
            {
                throw new OrderNotFoundException();
            }

            return order.MapToDto();
        }

        public async Task<GetOrderResponseDto> UpdateAsync(Guid orderId, UpdateOrderRequestDto updateOrderRequestDto)
        {
            if (orderId == Guid.Empty)
                throw new OrderArgumentException();

            if (updateOrderRequestDto == null)
                throw new ArgumentNullException(nameof(UpdateOrderRequestDto));

            if (await _ordersRepository.GetOrderByIdAsync(orderId) is null)
            {
                throw new OrderNotFoundException();
            }

            var dbOrder = updateOrderRequestDto.MapToDb();
            var updatedOrder = await _ordersRepository.UpdateOrderAsync(orderId, dbOrder);

            return updatedOrder.MapToDto();
        }

        public async Task DeleteAsync(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new OrderArgumentException();

            if (await _ordersRepository.GetOrderByIdAsync(orderId) is null)
                throw new OrderNotFoundException();

            await _ordersRepository.DeleteOrderAsync(orderId);
        }
    }
}
