namespace Api.Models
{
    public class Repository
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string language { get; set; }        
        public DateTime created_at { get; set; }
    }
}
