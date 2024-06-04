package iss.backend.Repository;

import iss.Models.Librarian;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RepoLibrarian extends JpaRepository<Librarian, Long>{
}
