using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Core.Repository
{
    public class RepositoryCompany
    { 
        private ApplicationDbContext _context { get; }
        private IMapper _mapper { get; }
        public RepositoryCompany(ApplicationDbContext context, IMapper mapper)
        {
                _context = context;
                _mapper = mapper;
        }
        public async Task Create(CompanyCreateDto dto)
        {
            if (dto.Name == string.Empty)
            {
                return;
            }
            var newCompany = _mapper.Map<Company>(dto);
            await _context.Companies.AddAsync(newCompany);
            await _context.SaveChangesAsync();
               
        }
        public async Task<IEnumerable<CompanyGetDto>> Get()
        {
            var companies = await _context
                .Companies.AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            var convertedCompanies = _mapper.Map<IEnumerable<CompanyGetDto>>(companies);  
            
            return convertedCompanies;
        }          
        public async Task Update(CompanyUpdateDto dto)
        {
            if (dto.Name == string.Empty)
            {
                return ;
            }
            var UpdateCompany = _mapper.Map<Company>(dto);

            await _context
                 .Companies
                 .Where(x => x.Id == UpdateCompany.Id)
                 .ExecuteUpdateAsync(x => x
                 .SetProperty(y => y.Name, y => dto.Name)
                 .SetProperty(y => y.Size, y => dto.Size));

            await _context.SaveChangesAsync();                
        }
        public async Task Delete( long id)
        {
            await _context
                .Companies
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }
        public async Task<IEnumerable<JobGetDto>> GetJobs(long idJob)
        {
            var job = await _context
                .Jobs
                .AsNoTracking()
                .Where(x=>x.CompanyId == idJob)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            
            var convertJob = _mapper.Map<IEnumerable<JobGetDto>>(job);

            return convertJob;
        }
    }
}
