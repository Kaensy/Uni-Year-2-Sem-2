package iss.backend.Repository;

import iss.Models.Subscriber;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RepoSubscriber extends JpaRepository<Subscriber, Long> {
}
