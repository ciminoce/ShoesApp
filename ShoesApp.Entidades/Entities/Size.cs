namespace ShoesApp.Entidades.Entities
{
    public class Size
    {
        public int SizeId { get; set; }
        public double SizeNumber { get; set; }  // Talle numérico (por ejemplo: 36, 37, 38, etc.)
        public bool Active { get; set; } = true;
        public ICollection<ShoeSize> ShoeSizes { get; set; } = new List<ShoeSize>();

    }
}
