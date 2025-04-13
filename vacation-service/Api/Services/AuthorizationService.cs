using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Dto.Authorization.Requests;
using Api.Dto.Authorization.Responses;
using Api.Dto.Orders.Requests;
using Api.Exceptions.Users;
using Api.Mappers;
using Api.Services.Interfaces;
using Common;
using Common.Roles;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ClaimTypes = System.Security.Claims.ClaimTypes;
using CustomClaimTypes = Common.ClaimTypes;

namespace Api.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUsersRepository _usersRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly IDepartmentsRepository _departmentsRepository;
    
    public AuthorizationService(IUsersRepository usersRepository, IDepartmentsRepository departmentsRepository, IOptions<JwtSettings> jwtSettings)
    {
        _usersRepository = usersRepository;
        _jwtSettings = jwtSettings.Value;
        _departmentsRepository = departmentsRepository;
    }

    public async Task<LoginSuccessResponse> RegisterAsync(RegisterRequestDto registerRequestDto)
    {
        if (registerRequestDto == null)
            throw new ArgumentNullException(nameof(RegisterRequestDto));

        if (await _usersRepository.GetByEmailAsync(registerRequestDto.Email) != null)
        {
            throw new EmailIsExistException();
        }
        
        var dbUser = registerRequestDto.MapToDb();
        dbUser.UserRole = UserRoles.Director; // Пофиксить
        var res = await _usersRepository.AddAsync(dbUser);
        
        var dbDepartment = await _departmentsRepository.CreateDepartmentAsync(new DbDepartment
        {
            Name="Главное направление",
            Description = "Самое первое направление",
            SupervisorId = res.Id,
            Role = DepartmentRoles.Management
        });
        
        res.DepartmentId = dbDepartment.Id;
        await _usersRepository.UpdateAsync(res);
        
        return new LoginSuccessResponse { AccessToken = GenerateAccessToken(res) };
    }

    public async Task<LoginSuccessResponse> LoginAsync(LoginRequestDto loginRequestDto)
    {
        if (loginRequestDto == null)
            throw new ArgumentNullException(nameof(LoginRequestDto));

        var res = await _usersRepository.GetByEmailAsync(loginRequestDto.Email);
        
        if (res is null)
        {
            throw new FailureAuthorizationRequestException();
        }

        if (!PasswordService.Verify(loginRequestDto.Password, res.HashedPassword))
        {
            throw new FailureAuthorizationRequestException();
        }

        return new LoginSuccessResponse { AccessToken = GenerateAccessToken(res) };
    }
    
    private string GenerateAccessToken(DbUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _jwtSettings.GetSymmetricSecurityKey();

        var claims = new[]
        {
            new Claim(CustomClaimTypes.Id.ToString(), user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.UserRole.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryInMinutes),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256)
        );

        return tokenHandler.WriteToken(token);
    }
}