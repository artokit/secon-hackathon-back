using DataAccess.Common.Interfaces.Dapper;
using DataAccess.Common.Interfaces.Repositories;
using DataAccess.Dapper;
using DataAccess.Models;

namespace DataAccess.Repositories;

public class UsersRepository : IUsersRepository
{
    private IDapperContext _dapperContext;
    
    public UsersRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<DbUser> AddAsync(DbUser dbUser)
    {
        var queryObject = new QueryObject(
    @"INSERT INTO users(name, surname, patronymic, hashed_password, email, role, department_id, position_name, hiring_date) 
      VALUES(@Name, @Surname, @Patronymic, @HashedPassword, @Email, @role, @DepartmentId, @PositionName, @HiringDate) 
      RETURNING id as ""Id"", name as ""Name"", surname as ""Surname"", image_name as ""ImageName"", patronymic as ""Patronymic"", email as ""Email"", role as ""UserRole"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", phone as ""Phone"", department_id as ""DepartmentId"", hiring_date as ""HiringDate"", position_name as ""PositionName""",
    new
    {
        dbUser.Name,
        dbUser.Surname,
        dbUser.Patronymic,
        HashedPassword = dbUser.HashedPassword,
        dbUser.Email,
        role = dbUser.UserRole,
        DepartmentId = dbUser.DepartmentId,
        HiringDate = dbUser.HiringDate,
        PositionName = dbUser.PositionName
    });


        var res = await _dapperContext.CommandWithResponse<DbUser>(queryObject);
        return res;
    }


    public async Task<DbUser?> GetByEmailAsync(string email)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", patronymic as ""Patronymic"", email as ""Email"", role as ""UserRole"", hashed_password as ""HashedPassword"" FROM USERS 
                 WHERE email=@email",
            new { email });
        
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }

    public async Task<DbUser?> GetByIdAsync(Guid userId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", surname as ""Surname"", image_name as ""ImageName"", patronymic as ""Patronymic"", email as ""Email"", role as ""UserRole"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", department_id as ""DepartmentId"",  phone as ""Phone"", position_name as ""PositionName"" FROM USERS WHERE id=@id",
            new
            {
                id = userId
            });
        
        return await _dapperContext.FirstOrDefault<DbUser>(queryObject);
    }

    public async Task DeleteAsync(Guid userId)
    {
        var queryObject = new QueryObject(
            @"DELETE FROM USERS WHERE id=@id",
            new { id = userId });
        
        await _dapperContext.Command(queryObject);
    }

    public async Task<DbUser> UpdateAsync(DbUser user)
    {
        
        var queryObject = new QueryObject(@"
            UPDATE users SET name = @Name, surname = @Surname, patronymic = @Patronymic, hashed_password = @HashedPassword, email = @Email, role = @Role, image_name = @ImageName, department_id = @DepartmentId, phone = @Phone, telegram_username = @TelegramUsername, position_name = @PositionName WHERE id = @Id
            RETURNING id as ""Id"", name as ""Name"", surname as ""Surname"", image_name as ""ImageName"", patronymic as ""Patronymic"", email as ""Email"", role as ""UserRole"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", department_id as ""DepartmentId"", phone as ""Phone"", position_name as ""PositionName""",
            new
            {
                Id=user.Id,
                Name=user.Name,
                Surname=user.Surname,
                Patronymic=user.Patronymic,
                HashedPassword=user.HashedPassword,
                Email=user.Email,
                Role=user.UserRole,
                ImageName=user.ImageName,
                DepartmentId=user.DepartmentId,
                Phone=user.Phone,
                TelegramUsername=user.TelegramUsername,
                PositionName=user.PositionName
            });
    
        return await _dapperContext.CommandWithResponse<DbUser>(queryObject);
    }

    public async Task<List<DbUser>> GetUsersByDepartmentIdAsync(Guid departmentId)
    {
        var queryObject = new QueryObject(
            @"SELECT id as ""Id"", name as ""Name"", surname as ""Surname"", image_name as ""ImageName"", patronymic as ""Patronymic"", email as ""Email"", role as ""UserRole"", hashed_password as ""HashedPassword"", telegram_username as ""TelegramUsername"", department_id as ""DepartmentId"",  phone as ""Phone"", hiring_date as ""HiringDate"", position_name as ""PositionName"" FROM USERS WHERE department_id=@DepartmentId",
            new
            {
                DepartmentId = departmentId
            });

        return await _dapperContext.ListOrEmpty<DbUser>(queryObject);
    }
}