package mpp.com.Repository;

import mpp.com.Domain.Track;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

public class RepoDBTrack implements RepositoryTrack{

    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();

    public RepoDBTrack(Properties props) {
        logger.info("Initializing RepoDBTrack with properties: {} ", props );
        dbUtils = new JdbcUtils(props);

    }

    @Override
    public Optional<Track> findOne(Long aLong) {
        logger.info("Finding Track with ID: {}",  aLong);

        if(aLong == null) {
                throw new IllegalArgumentException("Id must not be null");
            }

            try {
                var connection = dbUtils.getConnection();
                PreparedStatement statement = connection.prepareStatement("SELECT * FROM track WHERE id = ?");
                statement.setLong(1, aLong);
                ResultSet resultSet = statement.executeQuery();

                if(resultSet.next()) {
                    Long id = resultSet.getLong("id");
                    String name = resultSet.getString("name");
                    int minimumAge = resultSet.getInt("minimumAge");
                    int maximumAge = resultSet.getInt("maximumAge");
                    int distance = resultSet.getInt("distance");
                    Track track = new Track(name, minimumAge, maximumAge, distance);
                    track.setId(id);
                    return Optional.of(track);
                }
                return Optional.empty();
            }
            catch (SQLException e) {
                throw new RuntimeException(e);
            }
    }

    @Override
    public Iterable<Track> findAll() {
        logger.info("Finding All Tracks");
        Comparator<Track> comparator = Comparator.comparingLong(Track::getId);
        Set<Track> tracks = new TreeSet<>(comparator);

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM track");
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {

                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int minimumAge = resultSet.getInt("minimumAge");
                int maximumAge = resultSet.getInt("maximumAge");
                int distance = resultSet.getInt("distance");
                Track track = new Track(name, minimumAge, maximumAge, distance);
                track.setId(id);

                tracks.add(track);
            }
            return tracks;
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<Track> save(Track entity) {
        logger.info("Saving track: {}",  entity);
        if(entity == null) {
            throw new IllegalArgumentException("entity must not be null");
        }
        try{
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("INSERT INTO track (name, minimum_age, maximum_age, distance) VALUES (?, ?, ?, ?)");
            statement.setString(1, entity.getName());
            statement.setInt(2, entity.getMinimumAge());
            statement.setInt(3, entity.getMaximumAge());
            statement.setInt(4, entity.getDistance());
            statement.executeUpdate();
            return Optional.empty();
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<Track> delete(Long aLong) {
        return Optional.empty();
    }

    @Override
    public Optional<Track> update(Track entity) {
        return Optional.empty();
    }
}
