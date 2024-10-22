using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Core.Repository
{
    public class RepositoryCandidate
    {
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }

        public RepositoryCandidate(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(CandidateCreateDto dto)
        {

            if (dto.FirstName == string.Empty || dto.LastName == string.Empty ||
                dto.Email == string.Empty || dto.Phone == string.Empty)
            {
                return;
            }

            var newCandidate = _mapper.Map<Candidate>(dto);
            await _context.Candidates.AddAsync(newCandidate);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<CandidateGetDto>> Get()
        {
            var candidates = await _context
                .Candidates.AsNoTracking()
                .Include(candidate => candidate.Job)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            var convertedCompanies = _mapper.Map<IEnumerable<CandidateGetDto>>(candidates);
            return convertedCompanies;
        }

        public async Task Update( CandidateUpdateDto dto)
        {

            if (dto.FirstName == string.Empty || dto.LastName == string.Empty ||
                dto.Email == string.Empty || dto.Phone == string.Empty)
            {
                return;
            }

            var updateCandidate = _mapper.Map<Candidate>(dto);

            await _context
                .Candidates
                .Where(x => x.Id == updateCandidate.Id)
                .ExecuteUpdateAsync(x => x
                .SetProperty(y => y.FirstName, y => dto.FirstName)
                .SetProperty(y => y.LastName, y => dto.LastName)
                .SetProperty(y => y.Email, y => dto.Email)
                .SetProperty(y => y.Phone, y => dto.Phone)
                .SetProperty(y => y.CoverLetter, y => dto.CoverLetter)
                .SetProperty(y => y.JobId, y => dto.JobId)
                );
            await _context.SaveChangesAsync();           
        }
       
        public async Task Delete(long id)
        {
            await _context.Candidates.Where(x => x.Id == id).ExecuteDeleteAsync();                       
        }
    }
}
