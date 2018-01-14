using Shop.Core.Domain;

namespace Shop.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(string email);
        void Add(User user);
    }
}
