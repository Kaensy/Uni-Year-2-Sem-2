package mpp.com.Repository;
import mpp.com.Domain.Child;
import mpp.com.Domain.ChildTrackDTO;
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

        try(var connection = dbUtils.getConnection()) {
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM children WHERE id = ?");
            statement.setLong(1, aLong);
            ResultSet resultSet = statement.executeQuery();

            if(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                Child child = new Child(name, age);
                child.setId(id);
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

        try(var connection = dbUtils.getConnection()) {
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM children");
            ResultSet resultSet = statement.executeQuery();

            while(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("name");
                int age = resultSet.getInt("age");
                Child child = new Child(name, age);
                child.setId(id);

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
        
        try(var connection = dbUtils.getConnection()) {
            PreparedStatement statement = connection.prepareStatement(insertSql);
            statement.setString(1, entity.getName());
            statement.setInt(2, entity.getAge());
            statement.executeUpdate();
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
    public Iterable<ChildTrackDTO> getChildrenByTracks(long idTrack){
        return null;
    }
}
