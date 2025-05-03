using System.ComponentModel.DataAnnotations;

namespace CandidateAPI.Models
{
    public class ViewCandidateDto
    {
        public required string FirstName { get; set; }
        public required string CityName { get; set; }
        public required int YearOfJoining { get; set; }
    }
}
