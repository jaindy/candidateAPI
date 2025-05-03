using System.ComponentModel.DataAnnotations;

namespace CandidateAPI.Models
{
    public class AddCandidateDto
    {
        [Required(ErrorMessage = "First Name is required")]
     //   [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must contain letters only")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "City Name is required")]
      //  [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City name must contain letters and spaces only")]
        public required string CityName { get; set; }


        [Required(ErrorMessage = "Year Of Joining is required")]
     //   [StringLength(4, ErrorMessage = "YearOfJoining cannot be longer than 4 digits")]
        public required int YearOfJoining { get; set; }
    }
}
