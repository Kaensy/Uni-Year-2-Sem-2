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

            List<Child> childList = new List<Child>();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(reader.GetOrdinal("id"));
                    var name = reader.GetString(reader.GetOrdinal("name"));
                    var age = reader.GetInt32(reader.GetOrdinal("age"));
                    var child = new Child(id, name, age);
                    childList.Add(child);
                }
            }

            foreach (var child in childList)
            {
                using var commandTracks = new NpgsqlCommand("SELECT * FROM track t JOIN childrentracks ct ON t.id = ct.id_track WHERE ct.id_child = @idChild", connection);
                commandTracks.Parameters.AddWithValue("@idChild", child.Id);
                using var readerTracks = commandTracks.ExecuteReader();
                var tracks = new List<Track>();

                while (readerTracks.Read())
                {
                    var nameTrack = readerTracks.GetString(readerTracks.GetOrdinal("name"));
                    var minimumAge = readerTracks.GetInt32(readerTracks.GetOrdinal("minimum_age"));
                    var maximumAge = readerTracks.GetInt32(readerTracks.GetOrdinal("maximum_age"));
                    var distance = readerTracks.GetInt32(readerTracks.GetOrdinal("distance"));
                    var track = new Track(nameTrack, minimumAge, maximumAge, distance);
                    tracks.Add(track);
                }

                child.Tracks = tracks;
                children.Add(child);
            }
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
            throw;
        }

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

        var insertSql = "INSERT INTO children(name, age) VALUES (@name, @age) RETURNING id";

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@age", entity.Age);

            
            var childId = (int)command.ExecuteScalar();

            if (entity.Tracks != null) 
            {
                foreach (var track in entity.Tracks)
                {
                    using var commandTrack = new NpgsqlCommand("INSERT INTO childrentracks(id_child, id_track) VALUES (@idChild, @idTrack)", connection);
                    commandTrack.Parameters.AddWithValue("@idChild", childId);
                    commandTrack.Parameters.AddWithValue("@idTrack", track.Id);
                    commandTrack.ExecuteNonQuery();
                }
            }

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
    
    public IEnumerable<ChildTrackDTO> GetChildrenByTracks(long idTrack)
    {
        Logger.Info($"Finding Children that participate in Track with id: {idTrack}");
        var children = new List<ChildTrackDTO>();

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand("SELECT c.id, c.name, c.age FROM children c JOIN childrentracks ct ON c.id = ct.id_child WHERE ct.id_track = @idTrack", connection);
            command.Parameters.AddWithValue("@idTrack", idTrack);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("name"));
                var age = reader.GetInt32(reader.GetOrdinal("age"));
                var child = new Child(name, age) { Id = id };
                var childTrack = new ChildTrackDTO(child, (int)idTrack);

                children.Add(childTrack);
            }
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
            throw;
        }

        return children;
    }
    
    public IEnumerable<Child> GetChildrenByTrackId(long idTrack) {
    Logger.Info($"Finding Children that participate in Track with id: {idTrack}");
    var children = new SortedSet<Child>(Comparer<Child>.Create((x, y) => x.Id.CompareTo(y.Id)));

    try
    {
        var connection = DBUtil.Instance.GetConnection();
        using var command = new NpgsqlCommand("SELECT c.id, c.name, c.age FROM children c JOIN childrentracks ct ON c.id = ct.id_child WHERE ct.id_track = @idTrack", connection);
        command.Parameters.AddWithValue("@idTrack", idTrack);

        List<Child> childList = new List<Child>();
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("name"));
                var age = reader.GetInt32(reader.GetOrdinal("age"));
                var child = new Child(name, age) { Id = id };
                childList.Add(child);
            }
        }

        foreach (var child in childList)
        {
            using var commandTracks = new NpgsqlCommand("SELECT * FROM track t JOIN childrentracks ct ON t.id = ct.id_track WHERE ct.id_child = @idChild", connection);
            commandTracks.Parameters.AddWithValue("@idChild", child.Id);
            using var readerTracks = commandTracks.ExecuteReader();
            var tracks = new List<Track>();

            while (readerTracks.Read())
            {
                var nameTrack = readerTracks.GetString(readerTracks.GetOrdinal("name"));
                var minimumAge = readerTracks.GetInt32(readerTracks.GetOrdinal("minimum_age"));
                var maximumAge = readerTracks.GetInt32(readerTracks.GetOrdinal("maximum_age"));
                var distance = readerTracks.GetInt32(readerTracks.GetOrdinal("distance"));
                var track = new Track(nameTrack, minimumAge, maximumAge, distance) { Id = idTrack };
                tracks.Add(track);
            }
            child.Tracks = tracks;
            children.Add(child);
        }
    }
    catch (NpgsqlException e)
    {
        Logger.Error(e);
        throw;
    }

    return children;
}
}