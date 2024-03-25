using System.Collections.Generic;
using MPP_CSharp.Domain;

namespace MPP_CSharp.Repository
{
    public interface IRepository<TId, TE> where TE : Entity<TId>
    {
        TE? Search(TId id);
        IEnumerable<TE> GetAll();
        TE? Save(TE entity);
        TE Delete(TId id);
        TE Update(TE entity);
    }
}