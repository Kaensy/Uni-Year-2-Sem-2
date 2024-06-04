package iss.backend.Repository;

import iss.Models.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RepoBook extends JpaRepository<Book, Long>{
}
