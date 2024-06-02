package mpp.repository;

import mpp.Track;


import java.util.Optional;

public interface RepositoryTrack extends Repository<Long, Track>{

    Optional<Track> findTrackByName(String trackName);

    Iterable<Track> getTracksByAge(int minimumAge, int maximumAge);

}
