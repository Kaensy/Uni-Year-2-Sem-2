package transport.model;

public class TrackDTO {
    private Track track;
    private String trackName;

    private int nrParticipants;


    public TrackDTO(Track track, int nrParticipants) {
        this.track = track;
        this.trackName = track.getName();
        this.nrParticipants = nrParticipants;
    }

    public Track getTrack() {
        return track;
    }

    public void setTrack(Track track) {
        this.track = track;
    }

    public int getNrParticipants() {
        return nrParticipants;
    }

    public void setNrParticipants(int nrParticipants) {
        this.nrParticipants = nrParticipants;
    }

    public String getTrackName() {
        return trackName;
    }
}
