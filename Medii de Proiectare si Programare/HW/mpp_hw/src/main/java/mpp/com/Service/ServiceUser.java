package mpp.com.Service;

import mpp.com.Domain.User;
import mpp.com.Repository.RepoDBUser;
import mpp.com.Repository.RepositoryUser;

import java.util.Optional;

public class ServiceUser {


    RepositoryUser repoDBUser;

    public ServiceUser(RepositoryUser repoDBUser) {
        this.repoDBUser = repoDBUser;
    }

    public Optional<User> searchUsedByUsernamePassword(String username, String password) throws IllegalArgumentException {
        return repoDBUser.login(username, password);
    }

}
