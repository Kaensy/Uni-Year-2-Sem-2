package transport.model;

public class ChildTrackDTO {
    private Child child;
    private int trackNumber;

    public ChildTrackDTO(Child child, int trackNumber) {
        this.child = child;
        this.trackNumber = trackNumber;
    }

    public Child getChild() {
        return child;
    }

    public void setChild(Child child) {
        this.child = child;
    }

    public int getTrackNumber() {
        return trackNumber;
    }

    public void setTrackNumber(int trackNumber) {
        this.trackNumber = trackNumber;
    }

}
