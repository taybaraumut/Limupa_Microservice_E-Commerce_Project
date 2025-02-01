namespace Limupa.UI.Settings
{
    public class ClientSettings
    {
        public Client LimupaMemberClient { get; set; }
        public Client LimupaVisitorClient { get; set; }
        public Client LimupaAdminClient { get; set; }
    }

    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
