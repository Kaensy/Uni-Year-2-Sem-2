package mpp.com.Domain;

import java.util.List;
import java.util.Objects;

public class Track extends Entity<Long>{
    private String name;
    private int minimumAge;
    private int maximumAge;
    private int distance;
    private List<Child> children;

    public Track(String name, int minimumAge, int maximumAge, int distance) {
        this.name = name;
        this.minimumAge = minimumAge;
        this.maximumAge = maximumAge;
        this.distance = distance;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getMinimumAge() {
        return minimumAge;
    }

    public void setMinimumAge(int minimumAge) {
        this.minimumAge = minimumAge;
    }

    public int getMaximumAge() {
        return maximumAge;
    }

    public void setMaximumAge(int maximumAge) {
        this.maximumAge = maximumAge;
    }

    public int getDistance() {
        return distance;
    }

    public void setDistance(int distance) {
        this.distance = distance;
    }

    public List<Child> getChildren() {
        return children;
    }

    public void setChildren(List<Child> children) {
        this.children = children;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        if (!super.equals(o)) return false;
        Track track = (Track) o;
        return minimumAge == track.minimumAge && maximumAge == track.maximumAge && distance == track.distance && Objects.equals(name, track.name) && Objects.equals(children, track.children);
    }

    @Override
    public int hashCode() {
        return Objects.hash(super.hashCode(), name, minimumAge, maximumAge, distance, children);
    }

    @Override
    public String toString() {
        return "Track{" +
                "name='" + name + '\'' +
                ", minimumAge=" + minimumAge +
                ", maximumAge=" + maximumAge +
                ", distance=" + distance +
                ", children=" + children +
                ", id=" + id +
                '}';
    }
}
