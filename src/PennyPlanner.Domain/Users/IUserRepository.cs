namespace PennyPlanner.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default, bool asNoTracking = false);
        Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default, bool asNoTracking = false);

        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
