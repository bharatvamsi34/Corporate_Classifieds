namespace Classifieds.App.Models
{
    public class Fields
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string DataType { get; set; }
        public bool Mandatory { get; set; }
        public int Position { get; set; }
    }
}
