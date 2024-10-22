using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Job;
using backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Core.Repository
{
    public class RepositoryJob
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public RepositoryJob(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task Create(JobCreateDto dto)
        {
            if (dto.Title == string.Empty)
            {
                return;
            }
            var newJob = _mapper.Map<Job>(dto);
            await _context.Jobs.AddAsync(newJob);
            await _context.SaveChangesAsync();

        }
        
        public async Task<IEnumerable<JobGetDto>> Get()
        {
            var jobs = await _context
                .Jobs.AsNoTracking()
                .Include(job => job.Company)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var convertedCompanies = _mapper.Map<IEnumerable<JobGetDto>>(jobs);

            return convertedCompanies;
        }

        public async Task Update(JobUpdateDto dto)
        {
            if (dto.Title == string.Empty)
            {
                return;
            }
            var updateJob = _mapper.Map<Job>(dto);

            await _context
                .Jobs
                .Where(x => x.Id == updateJob.Id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.Title, y => dto.Title)
                .SetProperty(y => y.Level, y => dto.Level)
                .SetProperty(y => y.CompanyId, y => dto.CompanyId)
                );
            await _context.SaveChangesAsync();            
        }
        
        public async Task Delete( long id)
        {
            await _context.Jobs.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<CandidateGetDto>> GetCandidate(long idCandidate)
        {
            var candidates = await _context
                .Candidates
                .AsNoTracking()
                .Where(x => x.JobId == idCandidate)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var convertCandidate = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);

            return convertCandidate;
        }
    }
}

