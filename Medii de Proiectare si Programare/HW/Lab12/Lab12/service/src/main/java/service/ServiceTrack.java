package service;

import mpp.Track;
import mpp.repository.RepositoryTrack;

import java.util.Optional;

public class ServiceTrack {

    RepositoryTrack repoDBTrack;

    public ServiceTrack(RepositoryTrack repoDBTrack) {
        this.repoDBTrack = repoDBTrack;
    }

    public Optional<Track> saveTrack(String Name, int minimumAge, int maximumAge, int distance) {
        var track = new Track(Name, minimumAge, maximumAge, distance);
        return repoDBTrack.save(track);
    }

    public Optional<Track> updateTrack(Long id, String NewName, int NewMinimumAge, int NewMaximumAge, int NewDistance) {
        var track = new Track(NewName, NewMinimumAge, NewMaximumAge, NewDistance);
        track.setId(id);
        return repoDBTrack.update(track);
    }

    public Optional<Track> deleteTrack(Long id) {
        return repoDBTrack.delete(id);
    }

    public Optional<Track> searchTrackById(Long idTrack) throws IllegalArgumentException {
        return repoDBTrack.findOne(idTrack);
    }

    public Iterable<Track> getAllTracks() {
        return repoDBTrack.findAll();
    }

    public void addTrack(String name, int minimumAge, int maximumAge, int distance) {
        var track = new Track(name, minimumAge, maximumAge, distance);
        repoDBTrack.save(track);
    }

    public Optional<Track> findTrackByName(String trackName) {
        return repoDBTrack.findTrackByName(trackName);
    }

    public Iterable<Track> getTracksByAge(int minimumAge, int maximumAge){
        return repoDBTrack.getTracksByAge(minimumAge, maximumAge);
    }



}
