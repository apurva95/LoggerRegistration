namespace LoggerRegistration.Models
{
    [Serializable]
    public class RegistrationModel
    {
        public string CompanyName { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
        public string? ServiceName { get; set; }
        public string EmailId { get; set; } 
        public int ErrorAlerts { get; set; }
        public List<Email>? Emails { get; set; }
        public DateTime? LastEmailAlert { get; set; }
    }

    public class Email
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
    }
}
