using Shop.Core.Domain;
using System;

namespace Shop.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid id);
        User Get(string email);
        void Add(User user);
    }
}
