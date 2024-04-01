package mpp.com.Repository;
import mpp.com.Domain.Child;
import mpp.com.Domain.ChildTrackDTO;
import mpp.com.Domain.Track;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.util.*;
import java.sql.*;


public class RepoDBChild implements RepositoryChild
{
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();

    public RepoDBChild(Properties props) {
        logger.info("Initializing RepoDBChild with properties: {} ",props);
        dbUtils = new JdbcUtils(props);

    }
    
    @Override
    public Optional<Child> findOne(Long aLong) {
        logger.info("Finding Child with ID: {}",  aLong);
        if(aLong == null) {
            throw new IllegalArgumentException("Id must not be null");
        }

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM children WHERE id = ?");
            statement.setLong(1, aLong);
            ResultSet resultSet = statement.executeQuery();

            if(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                Child child = new Child(name, age);
                child.setId(id);

                PreparedStatement statementTracks = connection.prepareStatement("Select * from track t join childrentracks ct on t.id = ct.id_track where ct.id_child = ?");
                statementTracks.setLong(1, aLong);
                ResultSet resultSetTracks = statementTracks.executeQuery();
                List<Track> tracks = new ArrayList<>();

                while(resultSetTracks.next()) {
                    Long idTrack = resultSetTracks.getLong("id");
                    String nameTrack = resultSetTracks.getString("name");
                    int minimumAge = resultSetTracks.getInt("minimum_age");
                    int maximumAge = resultSetTracks.getInt("maximum_age");
                    int distance = resultSetTracks.getInt("distance");
                    Track track = new Track(nameTrack, minimumAge, maximumAge, distance);
                    track.setId(idTrack);
                    tracks.add(track);
                }
                child.setTracks(tracks);
                return Optional.of(child);
            }
            return Optional.empty();
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Iterable<Child> findAll() {
        logger.info("Finding all children");
        Comparator<Child> comparator = Comparator.comparingLong(Child::getId);
        Set<Child> children = new TreeSet<>(comparator);

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM children");
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                Child child = new Child(name, age);
                child.setId(id);

                PreparedStatement statementTracks = connection.prepareStatement("Select * from track t join childrentracks ct on t.id = ct.id_track where ct.id_child = ?");
                statementTracks.setLong(1, id);
                ResultSet resultSetTracks = statementTracks.executeQuery();
                List<Track> tracks = new ArrayList<>();

                while(resultSetTracks.next()) {
                    Long idTrack = resultSetTracks.getLong("id");
                    String nameTrack = resultSetTracks.getString("name");
                    int minimumAge = resultSetTracks.getInt("minimum_age");
                    int maximumAge = resultSetTracks.getInt("maximum_age");
                    int distance = resultSetTracks.getInt("distance");
                    Track track = new Track(nameTrack, minimumAge, maximumAge, distance);
                    track.setId(idTrack);
                    tracks.add(track);
                }
                child.setTracks(tracks);

                children.add(child);
            }
            return children;
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<Child> save(Child entity) {
        logger.info("Saving Child: {}", entity);
        if(entity == null) {
            throw new IllegalArgumentException("Entity must not be null");
        }
        
        String insertSql = "insert into Children(name, age) VALUES (?, ?)";
        
        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement(insertSql, Statement.RETURN_GENERATED_KEYS);
            statement.setString(1, entity.getName());
            statement.setInt(2, entity.getAge());
            statement.executeUpdate();

            ResultSet generatedKeys = statement.getGeneratedKeys();
            if (generatedKeys.next()) {
                entity.setId(generatedKeys.getLong(1));
            }

            PreparedStatement statementTracks = connection.prepareStatement("insert into childrentracks(id_child, id_track) VALUES (?, ?)");
            statementTracks.setLong(1, entity.getId());
            for(Track track : entity.getTracks()) {
                statementTracks.setLong(2, track.getId());
                statementTracks.executeUpdate();
            }

            return Optional.empty();
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<Child> delete(Long aLong) {
        return Optional.empty();
    }

    @Override
    public Optional<Child> update(Child entity) {
        return Optional.empty();
    }

    @Override
    public Iterable<ChildTrackDTO> getChildrenByTracks(Long idTrack){
        logger.info("Finding Children that participate in Track with id: {}",  idTrack);
        List<ChildTrackDTO> children = new ArrayList<>();

        try{
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT c.id, c.name, c.age FROM children c JOIN childrentracks ct ON c.id = ct.id_child WHERE ct.id_track = ?");
            statement.setLong(1, idTrack);
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                var child = new Child(name, age);
                child.setId(id);
                var childTrack = new ChildTrackDTO(child, Math.toIntExact(idTrack));

                children.add(childTrack);
            }
            return children;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    public Iterable<Child> getChildrenByTrackId(Long idTrack){
        logger.info("Finding Children that participate in Track with id: {}",  idTrack);
        Comparator<Child> comparator = Comparator.comparingLong(Child::getId);
        Set<Child> children = new TreeSet<>(comparator);

        try{
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT c.id, c.name, c.age FROM children c JOIN childrentracks ct ON c.id = ct.id_child WHERE ct.id_track = ?");
            statement.setLong(1, idTrack);
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                var child = new Child(name, age);
                child.setId(id);

                PreparedStatement statementTracks = connection.prepareStatement("Select * from track t join childrentracks ct on t.id = ct.id_track where ct.id_child = ?");
                statementTracks.setLong(1, id);
                ResultSet resultSetTracks = statementTracks.executeQuery();
                List<Track> tracks = new ArrayList<>();

                while(resultSetTracks.next()) {
                    String nameTrack = resultSetTracks.getString("name");
                    int minimumAge = resultSetTracks.getInt("minimum_age");
                    int maximumAge = resultSetTracks.getInt("maximum_age");
                    int distance = resultSetTracks.getInt("distance");
                    Track track = new Track(nameTrack, minimumAge, maximumAge, distance);
                    track.setId(idTrack);
                    tracks.add(track);
                }
                child.setTracks(tracks);

                children.add(child);
            }
            return children;
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }
}
