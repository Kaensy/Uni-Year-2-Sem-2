using System;
using System.Collections.Generic;
using MPP_CSharp.Domain;
using MPP_CSharp.Repository;

namespace MPP_CSharp.Service
{
    public class ServiceTrack
    {
        private readonly IRepositoryTrack _repoDBTrack;

        public ServiceTrack(IRepositoryTrack repoDBTrack)
        {
            _repoDBTrack = repoDBTrack ?? throw new ArgumentNullException(nameof(repoDBTrack));
        }

        public Track? SearchTrackById(long idTrack)
        {
            return _repoDBTrack.Search(idTrack);
        }

        public IEnumerable<Track> GetAllTracks()
        {
            return _repoDBTrack.GetAll();
        }

        public void AddTrack(string name, int minimumAge, int maximumAge, int distance)
        {
            var track = new Track(name, minimumAge, maximumAge, distance);
            _repoDBTrack.Save(track);
        }

        public Track? FindTrackByName(string trackName)
        {
            return _repoDBTrack.FindTrackByName(trackName);
        }

        public IEnumerable<Track> GetTracksByAge(int minimumAge, int maximumAge)
        {
            return _repoDBTrack.GetTracksByAge(minimumAge, maximumAge);
        }

        public IEnumerable<TrackDTO> GetTrackDTOs()
        {
            return _repoDBTrack.GetTrackDTOs();
        }

        public IEnumerable<TrackDTO> GetTrackDTOsByAge(int minimumAge, int maximumAge)
        {
            return _repoDBTrack.GetTrackDTOsByAge(minimumAge, maximumAge);
        }
    }
}