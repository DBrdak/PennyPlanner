namespace PennyPlanner.Infrastructure.Email.EmailDesign
{
    internal sealed record EmailSubject
    {
        internal string Value { get; init; }

        private EmailSubject(string value)
        {
            Value = value;
        }

        internal static EmailSubject WelcomeSubject = new("Welcome to Penny Planner!");
    }
}
