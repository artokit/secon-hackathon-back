using DataAccess.Models;

namespace Api.Services.Interfaces;

public interface IUserRightService
{
    public void CheckUserIsExist(DbUser? user);
}