using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShoesApp.Web.ViewModels.Colours
{
    public class ColourEditVm
    {
        public int ColourId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Color")]
        public string ColourName { get; set; } = null!;
        public bool Active { get; set; } = true;

    }
}
