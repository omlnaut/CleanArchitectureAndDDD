using BuberDiner.Domain.Entities;

namespace BuberDiner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    public User? GetByEmail(string email);
    public void Add(User user);
}
