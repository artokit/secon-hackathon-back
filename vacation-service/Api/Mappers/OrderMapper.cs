using Api.Dto.Orders.Requests;
using Api.Dto.Orders.Responses;
using DataAccess.Models;

namespace Api.Mappers
{
    public static class OrderMapper
    {
        public static DbOrder MapToDb(this CreateOrderRequestDto requestDto)
        {
            return new DbOrder
            {
                Creator_Id = requestDto.CreatorId,
                Departament_Id = requestDto.DepartamentId,
                Created_Data = requestDto.CreatedData,
                Status = requestDto.Status
            };
        }

        public static DbOrder MapToDb(this UpdateOrderRequestDto requestDto)
        {
            return new DbOrder
            {
                Creator_Id = requestDto.CreatorId,
                Departament_Id = requestDto.DepartamentId,
                Created_Data = requestDto.CreatedData,
                Status = requestDto.Status
            };
        }


        public static List<GetOrderResponseDto> MapToDto(this List<DbOrder> dbOrders)
        {
            return dbOrders.Select(x => x.MapToDto()).ToList();
        }

        public static GetOrderResponseDto MapToDto(this DbOrder dbOrder)
        {
            return new GetOrderResponseDto
            {
                Id = dbOrder.Id,
                CreatorId = dbOrder.Creator_Id,
                DepartamentId = dbOrder.Departament_Id,
                CreatedData = dbOrder.Created_Data,
                Status = dbOrder.Status
            };
        }

        public static DbOrder MapToDb(this UpdateOrderRequestDto order, DbOrder dbOrder)
        {
            return new DbOrder
            {
                Id = dbOrder.Id,
                Creator_Id = dbOrder.Creator_Id,
                Departament_Id = dbOrder.Departament_Id,
                Created_Data = dbOrder.Created_Data,
                Status = dbOrder.Status
            };
        }
    }
}