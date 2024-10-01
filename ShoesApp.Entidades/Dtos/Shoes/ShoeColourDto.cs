namespace ShoesApp.Entidades.Dtos.Shoes
{
    public class ShoeColourDto
    {
        public int ShoeColorId { get; set; }
        public int ShoeId { get; set; }
        public int ColourId { get; set; }
        public decimal Price { get; set; } // El precio específico del color para este zapato
    }

}
