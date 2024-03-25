using MPP_CSharp.Domain;

namespace MPP_CSharp.Repository;

public interface IRepositoryUser : IRepository<long,User>
{
    public User? FindByUsernamePassword(string username, string password);
}