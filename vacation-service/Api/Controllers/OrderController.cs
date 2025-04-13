using Api.Controllers.Abstractions;
using Api.Dto.Orders.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/orders")]
public class OrderController : BaseController
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [Authorize(Roles = "Hr,Director")]
    public async Task<IActionResult> Create(CreateOrderRequestDto createOrderRequestDto)
    {
        var result = await _orderService.CreateAsync(createOrderRequestDto);
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        var result = await _orderService.GetByIdAsync(orderId);
        return Ok(result);
    }

    [HttpPatch("{orderId}")]
    [Authorize(Roles = "Hr,Director")]
    public async Task<IActionResult> Update(Guid orderId, UpdateOrderRequestDto updateOrderRequestDto)
    {
        var result = await _orderService.UpdateAsync(orderId, updateOrderRequestDto);
        return Ok(result);
    }

    [HttpDelete("{orderId}")]
    [Authorize(Roles = "Hr,Director")]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        await _orderService.DeleteAsync(orderId);
        return NoContent();
    }
}
