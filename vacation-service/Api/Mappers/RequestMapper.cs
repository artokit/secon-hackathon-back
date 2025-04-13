using Api.Dto.Requests.Requests;
using Api.Dto.Requests.Responses;
using DataAccess.Models;

namespace Api.Mappers
{
    public static class RequestMapper
    {
        public static DbRequest MapToDb(this CreateRequestRequestDto requestDto)
        {
            return new DbRequest
            {
                User_Id = requestDto.UserId,
            };
        }

        public static DbRequest MapToDb(this UpdateRequestRequestDto requestDto)
        {
            return new DbRequest
            {
                User_Id = requestDto.UserId,
                Start_Date = requestDto.StartDate,
                End_Date = requestDto.EndDate,
                Extensions_Days = requestDto.ExtensionsDays,
                Fact_Date = requestDto.FactDate,
                Reason_Id = requestDto.ReasonId,
                Comment = requestDto.Comment
            };
        }


        public static List<GetRequestResponse> MapToDto(this List<DbRequest> dbRequests)
        {
            return dbRequests.Select(x => x.MapToDto()).ToList();
        }

        public static GetRequestResponse MapToDto(this DbRequest dbRequest)
        {
            return new GetRequestResponse
            {
                Id = dbRequest.Id,
                UserId = dbRequest.User_Id,
                StartDate = dbRequest.Start_Date,
                EndDate = dbRequest.End_Date,
                ExtensionsDays = dbRequest.Extensions_Days,
                FactDate = dbRequest.Fact_Date,
                ReasonId = dbRequest.Reason_Id,
                Comment = dbRequest.Comment
            };
        }

        public static DbRequest MapToDb(this UpdateRequestRequestDto requestDto, DbRequest dbRequest)
        {
            return new DbRequest
            {
                Id = dbRequest.Id,
                User_Id = dbRequest.User_Id,
                Start_Date = dbRequest.Start_Date,
                End_Date = dbRequest.End_Date,
                Extensions_Days = dbRequest.Extensions_Days,
                Fact_Date = dbRequest.Fact_Date,
                Reason_Id = dbRequest.Reason_Id,
                Comment = dbRequest.Comment
            };
        }
    }
}
