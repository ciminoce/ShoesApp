using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoesApp.Web.ViewModels.Shoes
{
    public class ShoeEditVm
    {
        public int ShoeId { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a brand")]
        [DisplayName("Brand")]

        public int BrandId { get; set; }

        [Required(ErrorMessage = "Sport is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a sport")]
        [DisplayName("Sport")]

        public int SportId { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a genre")]
        [DisplayName("Genre")]

        public int GenreId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]

        public string Model { get; set; } = null!;
        [MaxLength(2000, ErrorMessage = "{0} must have no more {1} characters")]

        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "{0} is required")]
        [Range(1, int.MaxValue)]
        [DisplayName("Unit Price")]

        public decimal Price { get; set; }


        public bool Active { get; set; }
        [ValidateNever]
        public List<SelectListItem>? Brands { get; set; }
        [ValidateNever]
        public List<SelectListItem>? Sports { get; set; }
        [ValidateNever]
        public List<SelectListItem>? Genres { get; set; }

    }
}
