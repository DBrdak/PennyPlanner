using System.ComponentModel.DataAnnotations;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Users.Events;

namespace Domestica.Budget.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        public string IdentityId { get; private set; }
        public Username Username { get; private set; }
        public Email Email { get; private set; }
        public DateTime CreatedAt { get; init; }

        private User(Username username, Email email) 
            : base(new UserId())
        {
            Username = username;
            Email = email;
            IdentityId = string.Empty;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(Username username, Email email)
        {
            var user = new User(username, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }

        public void SetIdentityId(string id)
        {
            IdentityId = id;
        }
    }
}
