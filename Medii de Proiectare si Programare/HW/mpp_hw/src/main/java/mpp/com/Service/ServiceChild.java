package mpp.com.Service;

import mpp.com.Domain.Child;
import mpp.com.Domain.ChildTrackDTO;
import mpp.com.Domain.Track;
import mpp.com.Repository.RepoDBChild;

import java.util.List;
import java.util.Optional;

public class ServiceChild {

    RepoDBChild repoDBChild;

    public ServiceChild(RepoDBChild repoDBChild) {
        this.repoDBChild = repoDBChild;
    }

    public Optional<Child> searchChildById(Long id) throws IllegalArgumentException {
        return repoDBChild.findOne(id);
    }

    public Iterable<Child> getAllChildren() {
        return repoDBChild.findAll();
    }

    public void addChild(String name, int age, List<Track> tracks) {

        var child = new Child(name, age);
        child.setTracks(tracks);
        repoDBChild.save(child);
    }

    public Iterable<ChildTrackDTO> getChildrenByTracks(Long idTrack) {
        return repoDBChild.getChildrenByTracks(idTrack);
    }

    public Iterable<Child> getChildrenByTrackId(Long idTrack) {
        return repoDBChild.getChildrenByTrackId(idTrack);
    }
}
