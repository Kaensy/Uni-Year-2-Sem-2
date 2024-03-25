using System.Collections.Generic;
using MPP_CSharp.Domain;

namespace MPP_CSharp.Repository
{
    public interface IRepositoryChild : IRepository<long, Child>
    {
        public IEnumerable<Child> FindChildrenByTrack(long trackId);
    }
}