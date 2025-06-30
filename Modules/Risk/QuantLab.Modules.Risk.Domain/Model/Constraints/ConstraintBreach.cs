namespace QuantLab.Modules.Risk.Domain.Model.Constraints
{
    internal class ConstraintBreach
    {

        internal string Message { get; }//just a dummy model to show the message
        internal DateTime BrachedOn { get; }
        private ConstraintBreach(string message, DateTime brachedOn)
        {
            Message = message;
            BrachedOn = brachedOn;
        }

        public override string? ToString()
        {
            return Message;
        }


        internal static ConstraintBreach Create(string message)
            => new ConstraintBreach(message, DateTime.UtcNow);
    }
}
