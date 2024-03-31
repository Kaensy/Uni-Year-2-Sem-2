package mpp.com.Service;

import mpp.com.Domain.User;
import mpp.com.Repository.RepoDBUser;

import java.util.Optional;

public class ServiceUser {


    RepoDBUser repoDBUser;

    public ServiceUser(RepoDBUser repoDBUser) {
        this.repoDBUser = repoDBUser;
    }

    public Optional<User> searchUsedByUsernamePassword(String username, String password) throws IllegalArgumentException {
        return repoDBUser.login(username, password);
    }

}
