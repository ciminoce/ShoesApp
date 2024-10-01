using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ShoesApp.Web.ViewModels.Sports
{
    public class SportEditVm
    {
        public int SportId { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must have between {2} and {1} characters", MinimumLength = 3)]
        [DisplayName("Sport Name")]
        public string SportName { get; set; } = null!;
        public bool Active { get; set; } = true;

    }
}
