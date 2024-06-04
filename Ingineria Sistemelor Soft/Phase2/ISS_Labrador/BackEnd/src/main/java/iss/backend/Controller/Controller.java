package iss.backend.Controller;


import iss.Models.Subscriber;
import iss.backend.Service.Service;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;


@RestController
@RequestMapping("/api")
public class Controller {

    @Autowired
    private Service service;

    @GetMapping("/subscribers")
    public ResponseEntity<List<Subscriber>> getAllSubscribers() {
        return ResponseEntity.ok().body(service.getAllSubscribers());
    }




}
