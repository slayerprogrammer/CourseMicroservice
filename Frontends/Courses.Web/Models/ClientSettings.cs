namespace Courses.Web.Models
{
    public class ClientSettings
    {
        public Client WebClient { get; set; }
        public Client WebClientWebUser { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
