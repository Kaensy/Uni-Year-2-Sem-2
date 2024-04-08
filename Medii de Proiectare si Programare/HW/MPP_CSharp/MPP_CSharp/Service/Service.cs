using System;
using System.Collections.Generic;
using MPP_CSharp.Domain;
using MPP_CSharp.Service;

namespace MPP_CSharp.Service
{
    public class Service
    {
        private readonly ServiceUser _serviceUser;
        private readonly ServiceTrack _serviceTrack;
        private readonly ServiceChild _serviceChild;

        public Service(ServiceUser serviceUser, ServiceTrack serviceTrack, ServiceChild serviceChild)
        {
            _serviceUser = serviceUser ?? throw new ArgumentNullException(nameof(serviceUser));
            _serviceTrack = serviceTrack ?? throw new ArgumentNullException(nameof(serviceTrack));
            _serviceChild = serviceChild ?? throw new ArgumentNullException(nameof(serviceChild));
        }

        public User? SearchUserByUsernamePassword(string username, string password)
        {
            return _serviceUser.SearchUserByUsernamePassword(username, password);
        }

        public Track? SearchTrackById(long idTrack)
        {
            return _serviceTrack.SearchTrackById(idTrack);
        }

        public IEnumerable<Track> GetAllTracks()
        {
            return _serviceTrack.GetAllTracks();
        }

        public void AddTrack(string name, int minimumAge, int maximumAge, int distance)
        {
            _serviceTrack.AddTrack(name, minimumAge, maximumAge, distance);
        }

        public Child? SearchChildById(long id)
        {
            return _serviceChild.SearchChildById(id);
        }

        public IEnumerable<Child> GetAllChildren()
        {
            return _serviceChild.GetAllChildren();
        }

        public void AddChild(string name, int age, string[] trackValues)
        {
            var tracks = new List<Track>();
            foreach (var trackName in trackValues)
            {
                var track = _serviceTrack.FindTrackByName(trackName);
                if (track == null || track.MinimumAge > age || track.MaximumAge < age)
                {
                    throw new Exception("The child's age is not in the track's age range");
                }
                tracks.Add(track);
            }
            _serviceChild.AddChild(name, age, tracks);
        }

        public IEnumerable<ChildTrackDTO> GetChildrenByTracks(long idTrack)
        {
            return _serviceChild.GetChildrenByTracks(idTrack);
        }

        public Track? FindTrackByName(string trackName)
        {
            return _serviceTrack.FindTrackByName(trackName);
        }

        public IEnumerable<Track> GetTracksByAge(int minimumAge, int maximumAge)
        {
            return _serviceTrack.GetTracksByAge(minimumAge, maximumAge);
        }

        public IEnumerable<Child> GetChildrenByTrackId(long idTrack)
        {
            return _serviceChild.GetChildrenByTrackId(idTrack);
        }

        public IEnumerable<TrackDTO> GetTrackDTOs()
        {
            return _serviceTrack.GetTrackDTOs();
        }

        public IEnumerable<TrackDTO> GetTrackDTOsByAge(int minimumAge, int maximumAge)
        {
            return _serviceTrack.GetTrackDTOsByAge(minimumAge, maximumAge);
        }
    }
}