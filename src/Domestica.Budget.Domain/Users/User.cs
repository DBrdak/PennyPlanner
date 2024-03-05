using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Users.Events;
using Money.DB;

namespace Domestica.Budget.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        public string IdentityId { get; private set; }
        public Email Email { get; private set; }
        public Currency Currency { get; private set; }
        public DateTime CreatedAt { get; init; }

        private User(Email email, Currency currency) 
            : base(new UserId())
        {
            Email = email;
            IdentityId = string.Empty;
            Currency = currency;
            CreatedAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        private User()
        { }

        public static User Create(Email email, Currency currency)
        {
            var user = new User(email, currency);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }

        public void SetIdentityId(string id)
        {
            IdentityId = id;
        }
    }
}
