package transport.persistence;

import mpp.com.Domain.User;

import java.util.Optional;

public interface RepositoryUser extends Repository<Long, User>{
    Optional<User> login(String username, String password);
}
