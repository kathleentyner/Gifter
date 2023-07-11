using Gifter.Models;

namespace Gifter.Repositories
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        List<Post> Search(string criterion, bool sortDescending); 
        List<Post> Hottest(DateTime criterion, bool sortDesc);
        Post GetById(int id);
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        List<Post> GetAllWithComments();
        Post GetByIdWithComments(int id);
        
    }
}