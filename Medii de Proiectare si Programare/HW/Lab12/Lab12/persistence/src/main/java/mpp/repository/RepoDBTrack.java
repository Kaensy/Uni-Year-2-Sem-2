package mpp.repository;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import mpp.Track;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

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

    public Optional<Track> findTrackByName(String trackName) {
        logger.info("Finding Track with name: {}",  trackName);

        if(trackName == null) {
            throw new IllegalArgumentException("trackName must not be null");
        }

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM track WHERE name = ?");
            statement.setString(1, trackName);
            ResultSet resultSet = statement.executeQuery();

            if(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int minimumAge = resultSet.getInt("minimum_age");
                int maximumAge = resultSet.getInt("maximum_age");
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
                int minimumAge = resultSet.getInt("minimum_age");
                int maximumAge = resultSet.getInt("maximum_age");
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

    public Iterable<Track> getTracksByAge(int minimumAge, int maximumAge){
        logger.info("Finding All Tracks by Age");
        Comparator<Track> comparator = Comparator.comparingLong(Track::getId);
        Set<Track> tracks = new TreeSet<>(comparator);

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM track WHERE minimum_age <= ? AND maximum_age >= ?");
            statement.setInt(1, minimumAge);
            statement.setInt(2, maximumAge);
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {

                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int minimumAgeTrack = resultSet.getInt("minimum_age");
                int maximumAgeTrack = resultSet.getInt("maximum_age");
                int distance = resultSet.getInt("distance");
                Track track = new Track(name, minimumAgeTrack, maximumAgeTrack, distance);
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
        logger.info("Deleting track with ID: {}",  aLong);

        if(aLong == null) {
            throw new IllegalArgumentException("Id must not be null");
        }

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("DELETE FROM track WHERE id = ?");
            statement.setLong(1, aLong);
            int affectedRows = statement.executeUpdate();

            if(affectedRows == 0) {
                return Optional.empty();
            } else {
                return findOne(aLong);
            }
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<Track> update(Track entity) {
        logger.info("Updating track: {}",  entity);
        if(entity == null) {
            throw new IllegalArgumentException("entity must not be null");
        }
        try{
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("UPDATE track SET name = ?, minimum_age = ?, maximum_age = ?, distance = ? WHERE id = ?");
            statement.setString(1, entity.getName());
            statement.setInt(2, entity.getMinimumAge());
            statement.setInt(3, entity.getMaximumAge());
            statement.setInt(4, entity.getDistance());
            statement.setLong(5, entity.getId());
            int affectedRows = statement.executeUpdate();
            if (affectedRows == 0) {
                return Optional.empty();
            } else {
                return Optional.of(entity);
            }
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
