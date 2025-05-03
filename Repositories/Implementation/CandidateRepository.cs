using Azure.Core;
using CandidateAPI.Data;
using CandidateAPI.Models;
using CandidateAPI.Models.Entities;
using CandidateAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.Repositories.Implementation
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDBContext dBContext;
        public CandidateRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<CandidateDetails> CreateAsync(AddCandidateDto candidateDto)
        {

            var candidate = new CandidateDetails() {
                FirstName = candidateDto.FirstName,
                CityName = candidateDto.CityName,
                YearOfJoining = candidateDto.YearOfJoining
            };
            await dBContext.CandidateDetail.AddAsync(candidate);
            await dBContext.SaveChangesAsync();
            return candidate;


        }

        public async Task<IEnumerable<ViewCandidateDto>> GetAsync()
        {
            var candidates = await dBContext.CandidateDetail.ToListAsync();
            var viewCandidates = candidates.Select(c => new ViewCandidateDto
            {
                FirstName = c.FirstName,
                CityName = c.CityName,
                YearOfJoining = c.YearOfJoining
            });
            return viewCandidates;
        }
    }
}
