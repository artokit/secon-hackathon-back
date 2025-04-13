using System.Data;
using Dapper;
using DataAccess.Common.Interfaces.Dapper;
using Npgsql;

namespace DataAccess.Dapper;

public class Transaction : ITransaction
{
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;
    
    public Transaction(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }

        _transaction = _connection.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction.Dispose();
        _connection.Close();
        _connection.Dispose();
    }

    public void Commit()
    {
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }

    public async Task Command(IQueryObject queryObject)
    {
        await _connection.ExecuteAsync(queryObject.Sql, queryObject.Parameters, _transaction);
    }

    public async Task<T> CommandWithResponse<T>(IQueryObject queryObject)
    {
        return await _connection.QueryFirstAsync<T>(queryObject.Sql, queryObject.Parameters, _transaction);
    }
}