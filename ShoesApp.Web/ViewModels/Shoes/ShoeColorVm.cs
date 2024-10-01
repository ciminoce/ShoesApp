namespace ShoesApp.Web.ViewModels.Shoes
{
    public class ShoeColorVm
    {
        public int ColourId { get; set; }
        public string ColourName { get; set; } = null!;
        public decimal Price { get; set; } // Precio específico para este zapato
        public bool IsSelected { get; set; } // Si el color fue seleccionado
    }

}
