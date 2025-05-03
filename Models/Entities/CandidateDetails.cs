

namespace CandidateAPI.Models.Entities
{
    public class CandidateDetails
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }
       
        public required string CityName { get; set; }

        public required int YearOfJoining { get; set; }

    }
}
