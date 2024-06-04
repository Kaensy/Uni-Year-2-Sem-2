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
@Table(name = "books", schema = "public")
public class Book {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id")
    private Long id;

    @OneToOne
    @JoinColumn(name = "library_id")
    private Library library;

    @NotEmpty(message = "Try again! Book Name cannot be empty")
    @Column(name="name",unique = true)
    private String name;

    @NotEmpty(message = "Try again! Quantity cannot be empty")
    @Column(name="quantity",unique = true)
    private Long quantity;

    @NotEmpty(message = "Try again! Author cannot be empty")
    @Column(name="author",unique = true)
    private String author;

}
