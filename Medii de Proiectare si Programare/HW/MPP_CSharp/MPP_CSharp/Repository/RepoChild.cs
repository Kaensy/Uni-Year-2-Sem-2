using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using MPP_CSharp.Domain;
using Npgsql;

namespace MPP_CSharp.Repository;

public class RepoChild : IRepositoryChild
{
    private static readonly ILog Logger =
        LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(RepoChild));

    public RepoChild()
    {
        Logger.Debug("Creating RepoChild");
    }

    public Child? Search(long id)
    {
        Logger.Info($"Finding Child with ID: {id}");

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand("SELECT * FROM children WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            var childId = reader.GetInt32(reader.GetOrdinal("id"));
            var name = reader.GetString(reader.GetOrdinal("name"));
            var age = reader.GetInt32(reader.GetOrdinal("age"));
            var child = new Child(childId,name, age);
            Logger.Debug($"Child found:{child}");
            return child;
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        }
        Logger.Debug("Child not found");
        return null;
    }

    public IEnumerable<Child> GetAll()
    {
        Logger.Info("Finding all children");
        var children = new SortedSet<Child>(Comparer<Child>.Create((x, y) => x.Id.CompareTo(y.Id)));

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand("SELECT * FROM children", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("name"));
                var age = reader.GetInt32(reader.GetOrdinal("age"));
                var child = new Child(id, name, age);

                children.Add(child);
            }
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        }
        Logger.Debug($"Children found: {children.Count}");
        return children;
    }

    public Child? Save(Child? entity)
    {
        Logger.Info($"Saving Child: {entity}");

        if (entity == null)
        {
            Logger.Error("Child is null");
            return null;
        }
        
        var insertSql = "INSERT INTO children(name, age) VALUES (@name, @age)";

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@age", entity.Age);
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        }
        Logger.Debug($"Child saved:{entity}");
        return entity;
    }

    public Child Delete(long id)
    {
        throw new System.NotImplementedException();
    }

    public Child Update(Child entity)
    {
        throw new System.NotImplementedException();
    }

    public IEnumerable<Child> FindChildrenByTrack(long trackId)
    {
        throw new System.NotImplementedException();
    }
}