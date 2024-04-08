using System.Collections.Generic;
using MPP_CSharp.Domain;

namespace MPP_CSharp.Repository
{
    public interface IRepositoryChild : IRepository<long, Child>
    {
        public IEnumerable<ChildTrackDTO> GetChildrenByTracks(long trackId);
        public IEnumerable<Child> GetChildrenByTrackId(long idTrack);
    }
}