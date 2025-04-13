using Api.Controllers.Abstractions;
using Api.Dto.Requests.Requests;
using Api.Services.Interfaces;
using Common;
using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("vacation-service/requests")]
public class RequestController : BaseController
{
    private readonly IRequestService _requestService;
    private readonly IDapperContext _dapperContext;
    private readonly IUsersRepository _usersRepository;
    private readonly IDepartmentsRepository _departmentsRepository;
    
    public RequestController(IRequestService requestService, IDapperContext dapperContext, IUsersRepository usersRepository, IDepartmentsRepository departmentsRepository)
    {
        _requestService = requestService;
        _dapperContext = dapperContext;
        _departmentsRepository = departmentsRepository;
        _usersRepository = usersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRequestRequestDto createRequestRequestDto)
    {
        var queryObject = new QueryObject(@"INSERT INTO REQUESTS(user_id, approver_id, order_id, status) values(@userId, @approverId, @orderId, @status) RETURNING id as ""Id"", user_id as ""UserId"", approver_id as ""ApproverId"", order_id as ""OrderId"", status as ""Status""",
            new
            {
                userId = UserId,
                approverId = Guid.Empty,
                orderId = Guid.Empty,
                status = RequestStatus.WaitingApprove
            });
        var res = await _dapperContext.CommandWithResponse<DbRequest>(queryObject);
        
        foreach (var range in createRequestRequestDto.Ranges)
        {
            var q = new QueryObject(
                "INSERT INTO ranges(start_date, end_date, request_id) values(@startDate, @endDate, @requestId)",
                new {
                    startDate=range.StartDate,
                    endDate=range.EndDate,
                    requestId=res.Id
                });
            await _dapperContext.Command(q);
        }
        
        return Ok(res);
    }

    [HttpGet("{id}")]
    [Authorize] 
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _requestService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(UpdateRequestRequestDto updateRequestRequestDto)
    {
        var queryObject = new QueryObject(
            "UPDATE REQUESTS SET status=@status where id=@id", new
            {
                status=updateRequestRequestDto.RequestStatus,
                id=updateRequestRequestDto.Id
            });
        
        await _dapperContext.Command(queryObject);
        
        return Ok();
    }

    public class GetAllRequestToApproveDto
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public List<RangeDto> Ranges { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllRequestToApprove(Guid id)
    {
        var query = new QueryObject(@"SELECT id as ""Id"", user_id as ""UserId"", approver_id as ""ApproverId"", order_id as ""OrderId"", status as ""Status"" FROM requests where approver_id=@approverId",
            new
            {
                approverId=UserId
            });

        var res = await _dapperContext.ListOrEmpty<DbRequestCustom>(query);
        var r = new List<GetAllRequestToApproveDto>();
        
        foreach (var req in res)
        {
            var user = await _usersRepository.GetByIdAsync(req.UserId);
            var department = await _departmentsRepository.GetDepartmentByIdAsync((Guid)user.DepartmentId);
            var rangeQueryObject = new QueryObject(@"SELECT start_date as ""StartDate"", end_date as ""EndDate"" from ranges where request_id=@requestId", new
            {
                requestId = req.Id
            });

            var resRange = await _dapperContext.ListOrEmpty<RangeDto>(rangeQueryObject);
            r.Add(new GetAllRequestToApproveDto
            {
                Id = req.Id,
                EmployeeName = $"{user.Name} {user.Surname} {user.Patronymic}",
                Position = user.PositionName,
                Department = department.Name,
                Ranges = resRange,
                RequestStatus = req.Status
            });
        }
        
        return Ok(r);
    }

    // [HttpGet("")]
    // public async Task<IActionResult> GetAll()
    // {
        // var queryObject = new QueryObject("SELECT * FROM requests where ");
        // var res = await _dapperContext.ListOrEmpty<>();
    // }
}
