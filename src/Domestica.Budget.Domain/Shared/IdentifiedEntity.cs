using System.Text.Json.Serialization;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Domain.Shared
{
    public class IdentifiedEntity<TId> : Entity<TId>
        where TId : EntityId, new()
    {
        public UserIdentityId UserId { get; init; }

        public IdentifiedEntity(UserIdentityId userId) : base(new TId())
        {
            UserId = userId;
        }

        [JsonConstructor]
        protected IdentifiedEntity()
        {
            
        }
    }
}
