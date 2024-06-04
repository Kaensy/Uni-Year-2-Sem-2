package iss.backend.Repository;

import iss.Models.Account;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface RepoAccount extends JpaRepository<Account, Long>{
}
