using Gifter.Models;

namespace Gifter.Repositories
{
    internal interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        void Add(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(int id);
    }
}