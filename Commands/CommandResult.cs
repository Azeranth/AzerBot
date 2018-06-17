namespace AzerBot.Commands
{
    public class CommandResult
    {
        public enum FailureReasonEnum { AccessDenied, CommandDisabled, InvalidArguments, Unknown }
        private bool successs = true;
        private FailureReasonEnum failureReason = FailureReasonEnum.Unknown;
        private string failureMessage = "Unknown Error Occured";
        public bool Successs { get => successs; set => successs = value; }
        public FailureReasonEnum FailureReason { get => failureReason; set => failureReason = value; }
        public string FailureMessage { get => failureMessage; set => failureMessage = value; }
    }
}