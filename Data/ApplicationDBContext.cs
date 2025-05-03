using System.Collections.Generic;
using CandidateAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CandidateAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        public DbSet<CandidateDetails> CandidateDetail { get; set; }
    }
}
