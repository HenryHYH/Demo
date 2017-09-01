namespace HelloWeb.MessageSystem.Core.Domain.Logging
{
    public class ExceptionMessage
    {
        public string FullName { get; set; }

        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public string TargetSite { get; set; }

        public string HelpLink { get; set; }

        public ExceptionMessage InnerMessage { get; set; }
    }
}
