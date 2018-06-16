namespace AzerBot.Commands
{
    public class CommandResult
    {
        public enum FailureReasonEnum {AccessDenied, CommandDisabled,Unknown}
        private bool successs;
        private FailureReasonEnum failureReason;
        public bool Successs { get => successs; set => successs = value; }
        public FailureReasonEnum FailureReason { get => failureReason; set => failureReason = value; }
    }
}