using Gifter.Models;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        UserProfile GetById(int id);
        UserProfile GetByIdWithPosts(int id);

        void Add(UserProfile userProfile);
        void Update(UserProfile userProfile);
        void Delete(int id);
    }
}