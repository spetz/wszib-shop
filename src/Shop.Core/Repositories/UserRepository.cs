using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core.Domain;

namespace Shop.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly static ISet<User> _users = new HashSet<User>
        {
            new User("user@shop.com", "secret"),
            new User("admin@shop.com", "secret", role: Role.Admin)
        };

        public User Get(Guid id)
            => _users.SingleOrDefault(x => x.Id == id);

        public User Get(string email)
            => _users.SingleOrDefault(x => 
                string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase));

        public void Add(User user)
            => _users.Add(user);
    }
}
