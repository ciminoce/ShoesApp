
namespace ShoesApp.Entidades.Entities
{
    public class Shoe
    {
        public int ShoeId { get; set; }
        public int BrandId { get; set; }
        public int SportId { get; set; }
        public int GenreId { get; set; }
        public string Model { get; set; } = null!;
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public Brand Brand { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public Genre Genre { get; set; } = null!;
        public bool Active { get; set; }
        public ICollection<ShoeSize> ShoeSizes { get; set; } = new List<ShoeSize>();

    }
}
