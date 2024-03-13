namespace PennyPlanner.Infrastructure.Email
{
    public sealed class EmailProviderOptions
    {
        public string Name { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
