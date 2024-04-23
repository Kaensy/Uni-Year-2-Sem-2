package mpp.com.Repository;

import mpp.com.Domain.Track;
import mpp.com.Domain.TrackDTO;

import java.util.Optional;

public interface RepositoryTrack extends Repository<Long, Track>{

    Optional<Track> findTrackByName(String trackName);

    Iterable<Track> getTracksByAge(int minimumAge, int maximumAge);
    public Iterable<TrackDTO> getTrackDTOs();
    public Iterable<TrackDTO> getTrackDTOsByAge(int minimumAge, int maximumAge);
}
