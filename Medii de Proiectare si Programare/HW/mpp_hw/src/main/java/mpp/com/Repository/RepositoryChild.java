package mpp.com.Repository;

import mpp.com.Domain.Child;
import mpp.com.Domain.ChildTrackDTO;

public interface RepositoryChild extends Repository<Long, Child>{

    public Iterable<ChildTrackDTO> getChildrenByTracks(Long idTrack);
}
