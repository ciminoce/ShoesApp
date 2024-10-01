using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoesApp.Web.ViewModels.Shoes
{
    public class ShoeAssignColoursVm
    {
        public int ShoeId { get; set; }
        public string Model { get; set; } = null!;
        public decimal BasePrice { get; set; } // Precio base del zapato

        // Lista de colores asignados con sus precios específicos para este zapato
        public List<ShoeColorVm> AvailableColours { get; set; } = new List<ShoeColorVm>();

        // Lista de colores disponibles para ser asignados al zapato
        public IEnumerable<SelectListItem> AllColours { get; set; } = new List<SelectListItem>();

        // ID del nuevo color a agregar
        public int? NewColourId { get; set; }

        // Precio del nuevo color
        public decimal? NewColourPrice { get; set; }
    }

}
