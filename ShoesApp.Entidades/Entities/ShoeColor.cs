namespace ShoesApp.Entidades.Entities
{
    public class ShoeColour
    {
        public int ShoeColourId { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; } = null!;
        public int ColourId { get; set; }
        public Colour Colour { get; set; } = null!;
        public decimal PriceAdjustment { get; set; } // Ajuste de precio para este color

        // public ICollection<ShoeSize> ShoeSizes { get; set; } = new List<ShoeSize>();
    }
}
