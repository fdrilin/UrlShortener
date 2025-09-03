namespace UrlShortener.Models
{
    public class AppSettings
    {
        public string? Server { get; set; }
        public string? Database { get; set; }
        public string? Uid { get; set; }
        public string? Password { get; set; }

        public AppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Server = config["ConnectionParams:server"];
            Database = config["ConnectionParams:database"];
            Uid = config["ConnectionParams:uid"];
            Password = config["ConnectionParams:password"];
        }
    }
}
