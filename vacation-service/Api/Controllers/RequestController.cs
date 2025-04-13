using Api.Controllers.Abstractions;
using Api.Dto.Requests.Requests;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/requests")]
public class RequestController : BaseController
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestRequestDto createRequestRequestDto)
    {
        var result = await _requestService.CreateAsync(createRequestRequestDto);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize] 
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _requestService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateRequestRequestDto updateRequestRequestDto)
    {
        var result = await _requestService.UpdateAsync(id, updateRequestRequestDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _requestService.DeleteAsync(id);
        return NoContent();
    }
}
