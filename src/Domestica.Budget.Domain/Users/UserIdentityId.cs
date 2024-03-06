using System.Text.Json.Serialization;

namespace Domestica.Budget.Domain.Users;

public record UserIdentityId
{
    public string Value { get; private set; }

    public UserIdentityId(string value)
    {
        Value = value;
    }

    [JsonConstructor]
    private UserIdentityId()
    { }

    internal void UdpateId(string value) => Value = value;
}