package mpp.com.Domain;

import java.util.List;

public class Child extends Entity<Long>{
    private String name;
    private int age;
    private List<Track> tracks;
    private int numberOfTracks;

    public Child(String name, int age) {
        this.name = name;
        this.age = age;
    }

    public Child(String name, int age, List<Track> tracks) {
        this.name = name;
        this.age = age;
        this.tracks = tracks;
        this.numberOfTracks = tracks.size();
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public List<Track> getTracks() {
        return tracks;
    }

    public void setTracks(List<Track> tracks) {
        this.tracks = tracks;
        this.numberOfTracks = tracks.size();
    }

    public int getNumberOfTracks() {
        return numberOfTracks;
    }
}
