package iss.backend.Repository;

import iss.Models.Library;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface RepoLibrary extends JpaRepository<Library, Long>{
}
