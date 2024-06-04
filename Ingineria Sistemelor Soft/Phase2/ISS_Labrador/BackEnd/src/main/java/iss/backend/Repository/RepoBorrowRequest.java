package iss.backend.Repository;

import iss.Models.BorrowRequest;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RepoBorrowRequest extends JpaRepository<BorrowRequest, Long>{
}
