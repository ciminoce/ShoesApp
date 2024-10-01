
namespace ShoesApp.Entidades.Entities
{
    public class ShoeSize
    {
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; } = null!;
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;
        public int QuantityInStock { get; set; }

    }
}
