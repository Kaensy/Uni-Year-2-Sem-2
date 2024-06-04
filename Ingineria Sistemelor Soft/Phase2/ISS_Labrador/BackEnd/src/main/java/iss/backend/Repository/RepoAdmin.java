package iss.backend.Repository;

import iss.Models.Admin;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface RepoAdmin extends JpaRepository<Admin, Long>{
}
