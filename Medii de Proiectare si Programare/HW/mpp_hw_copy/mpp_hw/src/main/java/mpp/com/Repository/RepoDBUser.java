package mpp.com.Repository;

import mpp.com.Domain.User;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.*;
import java.util.Optional;
import java.util.Properties;

public class RepoDBUser implements RepositoryUser{

    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();

    public RepoDBUser(Properties props) {
        logger.info("Initializing RepoDBUser with properties: {}", props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public Optional<User> findOne(Long aLong) {
        return Optional.empty();
    }

    @Override
    public Iterable<User> findAll() {
        return null;
    }

    @Override
    public Optional<User> save(User entity) {
        logger.info("Saving user: {}", entity);
        if(entity == null) {
            throw new IllegalArgumentException("Entity must not be null");
        }

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("INSERT INTO users (username, password) VALUES (?, ?)");
            statement.setString(1, entity.getUsername());
            statement.setString(2, entity.getPassword());
            statement.executeUpdate();
            return Optional.empty();
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

    @Override
    public Optional<User> delete(Long aLong) {
        return Optional.empty();
    }

    @Override
    public Optional<User> update(User entity) {
        return Optional.empty();
    }

    @Override
    public Optional<User> login(String username, String password){
        logger.info("Logging in user with username: {}", username);
        if(username.isBlank() || password.isBlank()) {
            throw new IllegalArgumentException("Username and password must not be empty");
        }

        try {
            var connection = dbUtils.getConnection();
            PreparedStatement statement = connection.prepareStatement("SELECT * FROM users WHERE username = ? AND password = ?");
            statement.setString(1, username);
            statement.setString(2, password);
            var resultSet = statement.executeQuery();

            if(resultSet.next()) {
                Long id = resultSet.getLong("id");
                String name = resultSet.getString("username");
                String pass = resultSet.getString("password");
                User user = new User(name, pass);
                user.setId(id);
                return Optional.of(user);
            }
            return Optional.empty();
        }
        catch (SQLException e) {
            throw new RuntimeException(e);
        }
    }

}
