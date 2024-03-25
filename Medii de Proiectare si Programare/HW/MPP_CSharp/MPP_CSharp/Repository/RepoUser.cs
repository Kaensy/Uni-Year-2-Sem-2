using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using log4net;
using MPP_CSharp.Domain;
using Npgsql;

namespace MPP_CSharp.Repository;

public class RepoUser : IRepositoryUser
{
    private static readonly ILog Logger =
        LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(RepoUser));

    public RepoUser()
    {
        Logger.Debug("Creating RepoUser");
    }

    public User Search(long id)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new System.NotImplementedException();
    }

    public User Save(User entity)
    {
        throw new System.NotImplementedException();
    }

    public User Delete(long id)
    {
        throw new System.NotImplementedException();
    }

    public User Update(User entity)
    {
        throw new System.NotImplementedException();
    }

    public User? FindByUsernamePassword(string username, string password)
    {
        Logger.Info($"Logging in user with username: {username}");
        string query = "SELECT * FROM users WHERE username = @username AND password = @password";

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("username"));
                var pass = reader.GetString(reader.GetOrdinal("password"));
                var user = new User(name, pass);
                user.Id = id;
                Logger.Debug($"User found: {user}");
                return user;
            }
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        } 
        Logger.Debug("User not found");
        return null;
    }
}