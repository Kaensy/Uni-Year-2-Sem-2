package mpp.com.Service;

import mpp.com.Domain.Child;
import mpp.com.Domain.ChildTrackDTO;
import mpp.com.Domain.Track;
import mpp.com.Domain.User;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

public class Service {

    ServiceUser serviceUser;
    ServiceTrack serviceTrack;
    ServiceChild serviceChild;

    public Service(ServiceUser serviceUser, ServiceTrack serviceTrack, ServiceChild serviceChild) {
        this.serviceUser = serviceUser;
        this.serviceTrack = serviceTrack;
        this.serviceChild = serviceChild;
    }

    public Optional<User> searchUsedByUsernamePassword(String username, String password) throws IllegalArgumentException {
        return serviceUser.searchUsedByUsernamePassword(username, password);
    }

    public Optional<Track> searchTrackById(Long idTrack) throws IllegalArgumentException {
        return serviceTrack.searchTrackById(idTrack);
    }

    public Iterable<Track> getAllTracks() {
        return serviceTrack.getAllTracks();
    }

    public void addTrack(String name, int minimumAge, int maximumAge, int distance) {
        serviceTrack.addTrack(name, minimumAge, maximumAge, distance);
    }

    public Optional<Child> searchChildById(Long id) throws IllegalArgumentException {
        return serviceChild.searchChildById(id);
    }

    public Iterable<Child> getAllChildren() {
        return serviceChild.getAllChildren();
    }

    public void addChild(String name, int age, String[] trackValues) {
        List<Track> tracks = new ArrayList<>();
        for(String trackName : trackValues) {
            try{
                var track = serviceTrack.findTrackByName(trackName);
                if (track.get().getMinimumAge() > age || track.get().getMaximumAge() < age)
                    throw new RuntimeException("The child's age is not in the track's age range");
                tracks.add(track.get());
            } catch (Exception e) {
                throw new RuntimeException(e.getMessage());
            }
        }
        serviceChild.addChild(name, age,tracks);
    }

    public Iterable<ChildTrackDTO> getChildrenByTracks(Long idTrack) {
        return serviceChild.getChildrenByTracks(idTrack);
    }

    public Optional<Track> findTrackByName(String trackName) {
        return serviceTrack.findTrackByName(trackName);
    }

}
