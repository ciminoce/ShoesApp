using ShoesApp.Entidades.Entities;
using System.ComponentModel;

namespace ShoesApp.Web.ViewModels.Shoes
{
    public class ShoeListVm
    {
        public int ShoeId { get; set; }
        public string? Brand { get; set; }
        public string? Sport { get; set; }
        public string? Genre { get; set; }
        public string? Model { get; set; } 

        public decimal Price { get; set; }
        public bool Active { get; set; }

    }
}
