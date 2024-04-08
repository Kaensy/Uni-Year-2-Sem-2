using System;
using System.Collections.Generic;
using MPP_CSharp.Domain;
using MPP_CSharp.Repository;

namespace MPP_CSharp.Service
{
    public class ServiceChild
    {
        private readonly IRepositoryChild _repoDBChild;

        public ServiceChild(IRepositoryChild repoDBChild)
        {
            _repoDBChild = repoDBChild ?? throw new ArgumentNullException(nameof(repoDBChild));
        }

        public Child? SearchChildById(long id)
        {
            return _repoDBChild.Search(id);
        }

        public IEnumerable<Child> GetAllChildren()
        {
            return _repoDBChild.GetAll();
        }

        public void AddChild(string name, int age, List<Track> tracks)
        {
            var child = new Child(name, age) { Tracks = tracks };
            _repoDBChild.Save(child);
        }

        public IEnumerable<ChildTrackDTO> GetChildrenByTracks(long idTrack)
        {
            return _repoDBChild.GetChildrenByTracks(idTrack);
        }

        public IEnumerable<Child> GetChildrenByTrackId(long idTrack)
        {
            return _repoDBChild.GetChildrenByTrackId(idTrack);
        }
    }
}