import mpp.com.Domain.*;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

public class Tests {

    @Test
    @DisplayName("Domain Tests")
    public void domainTests() {
        Entity<Integer> entity = new Entity<>();
        entity.setId(1);
        assert entity.getId() == 1;

        Child child = new Child("name", 10);
        child.setId(1L);

        assert child.getName().equals("name");
        assert child.getAge() == 10;
        assert child.getId() == 1L;

        Child child2 = new Child("name", 10);
        child2.setId(1L);

        assert child.equals(child2);

        Track track = new Track("name", 8,12, 100);
        track.setId(1L);

        assert track.getName().equals("name");
        assert track.getMinimumAge() == 8;
        assert track.getMaximumAge() == 12;
        assert track.getDistance() == 100;
        assert track.getId() == 1L;

        Track track2 = new Track("name", 8,12, 100);
        track2.setId(1L);

        assert track.equals(track2);



    }


}
