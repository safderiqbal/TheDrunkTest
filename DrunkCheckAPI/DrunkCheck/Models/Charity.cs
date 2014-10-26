namespace DrunkCheck.Models
{
    public class Charity
    {
        public int Id { get; set; }

        public string ApiKey { get; set; }

        public Charity(string key)
        {
            ApiKey = key;
        }
    }
}