using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShoesApp.Web.ViewModels.Sizes
{
    public class SizeEditVm
    {
        public int SizeId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [Range(10.0,60.0, ErrorMessage = "{0} must be between {2} and {1} ")]
        [DisplayName("Size No.")]
        public double SizeNumber { get; set; }
        public bool Active { get; set; } = true;

    }
}
