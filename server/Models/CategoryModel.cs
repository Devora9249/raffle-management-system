namespace server.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GiftModel> Gifts { get; set; }
    }
}