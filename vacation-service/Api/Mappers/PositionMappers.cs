using Api.Dto.Positions.Requests;
using Api.Dto.Positions.Responses;
using DataAccess.Models;
using Npgsql.Replication.PgOutput.Messages;

namespace Api.Mappers;

public static class PositionMappers
{
    public static DbPosition MapToDb(this CreatePositionRequestDto positionRequestDto)
    {
        return new DbPosition
        {
            DepartmentId = positionRequestDto.DepartmentId,
            Name = positionRequestDto.Name
        };
    }
    
    public static DbPosition MapToDb(this UpdatePositionRequestDto positionRequestDto, DbPosition dbposition)
    {
        return new DbPosition()
        {
            Id = dbposition.Id,
            DepartmentId = positionRequestDto.DepartmentId ?? dbposition.DepartmentId,
            Name = positionRequestDto.Name ?? dbposition.Name
        };
    }

    public static GetPositionResponseDto MapToDto(this DbPosition position)
    {
        return new GetPositionResponseDto
        {
            Id = position.Id,
            DepartmentId = position.DepartmentId,
            Name = position.Name
        };
    }

    public static List<GetPositionResponseDto> MapToDto(this List<DbPosition> dbPositions)
    {
        return dbPositions.Select(x => x.MapToDto()).ToList();
    }
}