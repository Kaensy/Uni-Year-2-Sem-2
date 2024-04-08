using System.Collections.Generic;
using MPP_CSharp.Domain;

namespace MPP_CSharp.Repository
{
    public interface IRepositoryTrack : IRepository<long, Track>
    {
        Track? FindTrackByName(string trackName);
        IEnumerable<Track> GetTracksByAge(int minimumAge, int maximumAge);
        IEnumerable<TrackDTO> GetTrackDTOs();
        IEnumerable<TrackDTO> GetTrackDTOsByAge(int minimumAge, int maximumAge);
    }
}