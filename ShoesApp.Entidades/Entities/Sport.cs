namespace ShoesApp.Entidades.Entities
{
    public class Sport
    {
        public int SportId { get; set; }
        public string SportName { get; set; } = null!;
        public bool Active { get; set; } = true;
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
