using Api.Controllers.Abstractions;
using Api.Dto.Positions.Requests;
using Api.Mappers;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/positions")]
public class PositionController : BaseController
{
    private IPositionService _servicePosition;

    public PositionController(IPositionService positionService)
    {
        _servicePosition = positionService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _servicePosition.GetPositionsByDepartmentIdAsync(UserId));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePositionRequestDto positionRequestDto)
    {
        return Ok(await _servicePosition.AddPositionAsync(positionRequestDto));
    }

    [HttpPatch]
    public async Task<IActionResult> Update(UpdatePositionRequestDto positionRequestDto)
    {
        return Ok(await _servicePosition.UpdatePositionAsync(positionRequestDto));
    }

    [HttpDelete("{positionsId}")]
    public async Task<IActionResult> Delete(Guid positionsId)
    {
        await _servicePosition.DeletePositionAsync(positionsId);
        return NoContent();
    }
}