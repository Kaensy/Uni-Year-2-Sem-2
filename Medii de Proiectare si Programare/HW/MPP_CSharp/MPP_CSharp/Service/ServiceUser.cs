using System;
using MPP_CSharp.Domain;
using MPP_CSharp.Repository;

namespace MPP_CSharp.Service
{
    public class ServiceUser
    {
        private readonly IRepositoryUser _repoDBUser;

        public ServiceUser(IRepositoryUser repoDBUser)
        {
            _repoDBUser = repoDBUser ?? throw new ArgumentNullException(nameof(repoDBUser));
        }

        public User? SearchUserByUsernamePassword(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username must not be null or empty", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password must not be null or empty", nameof(password));
            }

            return _repoDBUser.FindByUsernamePassword(username, password);
        }
    }
}