package mpp.com;

import mpp.com.Domain.Child;
import mpp.com.Domain.Track;
import mpp.com.Domain.User;
import mpp.com.Repository.*;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;
import java.util.stream.StreamSupport;

public class Main {
    public static void main(String[] args) {

        Properties props = new Properties();
        try{
            props.load(new FileReader("bd.config"));
        } catch (IOException e) {
            throw new RuntimeException(e);
        }


        var RepoDBTrack = new RepoDBTrack(props);
        var RepoDBUser = new RepoDBUser(props);
        var RepoDBChild = new RepoDBChild(props);

        RepoDBTrack.save(new Track("track1", 6, 8,100));
        assert(RepoDBTrack.findOne(7L).equals(new Track("track1", 6, 8,100)));
        assert(StreamSupport.stream(RepoDBTrack.findAll().spliterator(),false).toList().size()==7);

        RepoDBChild.save(new Child("child1", 6));
        assert(RepoDBChild.findOne(1L).equals(new Child("child1", 6)));
        assert(StreamSupport.stream(RepoDBChild.findAll().spliterator(),false).toList().size()==1);

        RepoDBUser.save(new User("user1", "pass1"));
        assert(RepoDBUser.findOne(1L).equals(new User("user1", "pass1")));
        assert(StreamSupport.stream(RepoDBUser.findAll().spliterator(),false).toList().size()==1);




    }
}