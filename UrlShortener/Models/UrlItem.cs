using System.Text.Json.Serialization;

namespace UrlShortener.Models
{
    public class UrlItem
    {
        public int Id { get; set; }
        public string Full { get; set; }
        public string? Short { get; set; }
        public int CreatedById { get; set; }
        public string? CreatedAt { get; set; }

        [JsonConstructor]
        public UrlItem (int id, string full, string Short, int createdById, string createdAt)
        {
            Id = id;
            Full = full;
            this.Short = Short;
            CreatedById = createdById;
            CreatedAt = createdAt;
        }

        public UrlItem(int id, string full, string Short, int createdById)
        {
            Id = id;
            Full = full;
            this.Short = Short;
            CreatedById = createdById;
            CreatedAt = DateTime.Now.ToString();
        }
    }
}
