using System.Collections.Generic;
using System.Reflection;
using log4net;
using MPP_CSharp.Domain;
using Npgsql;

namespace MPP_CSharp.Repository;

public class RepoTrack : IRepositoryTrack
{
    private static readonly ILog Logger =
        LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(RepoTrack));

    public RepoTrack()
    {
        Logger.Debug("Creating RepoTrack");
    }

    public Track? Search(long id)
    {
        Logger.Info($"Finding Track with ID: {id}");

        try
        {
            var connection = DBUtil.Instance.GetConnection();

            using var command = new NpgsqlCommand("SELECT * FROM track WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            
            using var reader = command.ExecuteReader();
            if (!reader.Read()) return null;
            var trackId = reader.GetInt32(reader.GetOrdinal("id"));
            var name = reader.GetString(reader.GetOrdinal("name"));
            var minimumAge = reader.GetInt32(reader.GetOrdinal("minimum_age"));
            var maximumAge = reader.GetInt32(reader.GetOrdinal("maximum_age"));
            var distance = reader.GetInt32(reader.GetOrdinal("distance"));
            var track = new Track(trackId,name, minimumAge, maximumAge, distance);
            Logger.Debug($"Track found:{track}");
            return track;

        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        }
        Logger.Debug("Track not found");
        return null;
    }


    public IEnumerable<Track> GetAll()
    {
        Logger.Info("Finding all tracks");
        var tracks = new SortedSet<Track>(Comparer<Track>.Create((x, y) => x.Id.CompareTo(y.Id)));

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand("SELECT * FROM track", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("id"));
                var name = reader.GetString(reader.GetOrdinal("name"));
                var minimumAge = reader.GetInt32(reader.GetOrdinal("minimum_age"));
                var maximumAge = reader.GetInt32(reader.GetOrdinal("maximum_age"));
                var distance = reader.GetInt32(reader.GetOrdinal("distance"));
                var track = new Track(id,name, minimumAge, maximumAge, distance);
                tracks.Add(track);
            }
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
        }
        Logger.Debug($"Tracks found: {tracks.Count}");
        return tracks;
    }

    public Track? Save(Track entity)
    {
        Logger.Info($"Saving Track: {entity}");
        
        const string insertSql = "INSERT INTO track (name, minimum_age, maximum_age, distance) VALUES (@name, @minimumAge, @maximumAge, @distance)";

        try
        {
            var connection = DBUtil.Instance.GetConnection();
            using var command = new NpgsqlCommand(insertSql, connection);
            command.Parameters.AddWithValue("@name", entity.Name);
            command.Parameters.AddWithValue("@minimumAge", entity.MinimumAge);
            command.Parameters.AddWithValue("@maximumAge", entity.MaximumAge);
            command.Parameters.AddWithValue("@distance", entity.Distance);
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException e)
        {
            Logger.Error(e);
            return null;
        }
        Logger.Debug("Track saved");
        return entity;
    }

    public Track Delete(long id)
    {
        throw new System.NotImplementedException();
    }

    public Track Update(Track entity)
    {
        throw new System.NotImplementedException();
    }
}