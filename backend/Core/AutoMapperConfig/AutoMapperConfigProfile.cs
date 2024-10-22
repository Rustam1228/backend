using AutoMapper;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Entities;

namespace backend.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile:Profile
    {
        public AutoMapperConfigProfile()
        {
            //Company
            CreateMap<CompanyCreateDto,Company>();
            CreateMap<Company, CompanyGetDto>();
            CreateMap<CompanyUpdateDto, Company>();
            //Candidate
            CreateMap<CandidateCreateDto, Candidate>();
            CreateMap<CandidateUpdateDto, Candidate>();
            CreateMap<Candidate, CandidateGetDto>()
                .ForMember
                (
                deist => deist.JobTitle, opt => opt.MapFrom(src => src.Job.Title)
                );
            //Job
            CreateMap<JobCreateDto, Job>();
            CreateMap<JobUpdateDto, Job>();
            CreateMap<Job,JobGetDto>()
                .ForMember
                (
                deist=>deist.CompanyName,opt=>opt.MapFrom(src=>src.Company.Name)
                );
        }
    }
}
