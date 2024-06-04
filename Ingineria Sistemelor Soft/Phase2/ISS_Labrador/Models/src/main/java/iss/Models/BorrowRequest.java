package iss.Models;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotEmpty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.validator.constraints.Length;

import java.time.LocalDateTime;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@Table(name = "borrow_requests", schema = "public")
public class BorrowRequest {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id")
    private Long id;

    @OneToOne
    @JoinColumn(name = "book_id")
    private Book book;

    @OneToOne
    @JoinColumn(name = "account_id")
    private Account account;

    @NotEmpty(message = "Try again! Borrow Date cannot be empty")
    @Column(name="borrow_date")
    private LocalDateTime borrow_date;

    @NotEmpty(message = "Try again! Return Date cannot be empty")
    @Column(name="return_date")
    private LocalDateTime return_date;


}
