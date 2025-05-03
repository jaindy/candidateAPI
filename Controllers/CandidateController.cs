using CandidateAPI.Models;
using CandidateAPI.Models.Entities;
using CandidateAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;

namespace CandidateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository candidateRepository;
        public CandidateController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCandidates()
        {

            var candidate = await candidateRepository.GetAsync();
            if (candidate == null || !candidate.Any())
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "No candidates found.",
                    Path = HttpContext.Request.Path
                });
            }
            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> AddCandidate([FromBody] AddCandidateDto addCandidateDto)
        {

            var minYear = DateTime.Now.Year - 5;
            if (addCandidateDto.YearOfJoining < minYear)
            {
                return BadRequest(new
                {
                    errors = new[]
                    {
                    new { msg = $"Year of joining must be greater than {minYear}", param = "YearOfJoining" }
                }
                });
            }

                await candidateRepository.CreateAsync(addCandidateDto);

                return Ok(new { message = "Candidate saved successfully" });

        }
    }
}
