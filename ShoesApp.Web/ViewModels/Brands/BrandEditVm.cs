using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShoesApp.Web.ViewModels.Brands
{
    public class BrandEditVm
    {
        public int BrandId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; } = null!;
        [DisplayName("Image")]
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        [DisplayName("Remove Image")]
        public bool RemoveImage { get; set; }
        public bool Active { get; set; } = true;

    }
}
