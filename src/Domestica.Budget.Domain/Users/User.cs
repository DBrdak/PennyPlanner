﻿using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Users.Events;
using Money.DB;

namespace Domestica.Budget.Domain.Users
{
    public sealed class User : Entity<UserId>
    {
        public Username Username { get; init; }
        public Email Email { get; private set; }
        public Currency Currency { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; init; }

        private User(Username username, Email email, Currency currency, string passwordHash) 
            : base(new UserId())
        {
            Username = username;
            Email = email;
            Currency = currency;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
        }

        [JsonConstructor]
        private User(Username username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
        }

        public static User Create(Username username, Email email, Currency currency, string passwordHash)
        {
            var user = new User(username, email, currency, passwordHash);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
