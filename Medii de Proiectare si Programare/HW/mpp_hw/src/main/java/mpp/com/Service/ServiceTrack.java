package mpp.com.Service;

import mpp.com.Domain.Track;
import mpp.com.Repository.RepoDBTrack;

import java.util.Optional;

public class ServiceTrack {

    RepoDBTrack repoDBTrack;

    public ServiceTrack(RepoDBTrack repoDBTrack) {
        this.repoDBTrack = repoDBTrack;
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
}
