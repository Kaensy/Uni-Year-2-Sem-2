package mpp.com.Service;

import mpp.com.Domain.Track;
import mpp.com.Domain.TrackDTO;
import mpp.com.Repository.RepoDBTrack;
import mpp.com.Repository.RepositoryTrack;

import java.util.Optional;

public class ServiceTrack {

    RepositoryTrack repoDBTrack;

    public ServiceTrack(RepositoryTrack repoDBTrack) {
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

    public Iterable<Track> getTracksByAge(int minimumAge, int maximumAge){
        return repoDBTrack.getTracksByAge(minimumAge, maximumAge);
    }

    public Iterable<TrackDTO> getTrackDTOs() {
        return repoDBTrack.getTrackDTOs();
    }
    public Iterable<TrackDTO> getTrackDTOsByAge(int minimumAge, int maximumAge) {
        return repoDBTrack.getTrackDTOsByAge(minimumAge, maximumAge);
    }
}
