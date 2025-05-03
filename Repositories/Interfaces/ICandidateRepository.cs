using CandidateAPI.Models;
using CandidateAPI.Models.Entities;

namespace CandidateAPI.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task<CandidateDetails> CreateAsync(AddCandidateDto candidateDto);
        Task<IEnumerable<ViewCandidateDto>> GetAsync();
    }
}
