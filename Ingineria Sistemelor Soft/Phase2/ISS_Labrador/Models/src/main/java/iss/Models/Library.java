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
@Table(name = "libraries", schema = "public")
public class Library {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name="id")
    private Long id;

    @NotEmpty(message = "Try again! Name cannot be empty")
    @Column(name="name")
    private String name;

    @NotEmpty(message = "Try again! Address cannot be empty")
    @Column(name="address")
    private String address;


}
