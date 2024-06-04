package iss.Models;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotEmpty;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.validator.constraints.Length;


@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@Table(name = "librarians", schema = "public")
public class Librarian {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id")
    private Long id;

    @OneToOne
    @JoinColumn(name = "account_id")
    private Account account;

    @NotEmpty(message = "Try again! Name cannot be empty")
    @Column(name="full_name")
    private String fullName;

    @Length(min = 4, max = 20, message = "Phone number must be between 4 and 20 characters")
    @NotEmpty(message = "Try again! Phone Number cannot be empty")
    @Column(name="phone_number")
    private String phoneNumber;

}
