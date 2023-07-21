using BuberDiner.Application.Common.Interfaces.Persistence;
using BuberDiner.Domain.Entities;

namespace BuberDiner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = new();
    public void Add(User user)
    {
        Users.Add(user);
    }

    public User? GetByEmail(string email)
    {
        return Users.SingleOrDefault(user => user.Email == email);
    }
}
