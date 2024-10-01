namespace ShoesApp.Entidades.Entities
{
    public class Colour
    {
        public int ColourId { get; set; }
        public string ColourName { get; set; } = null!;
        public bool Active { get; set; } = true;
        //public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
