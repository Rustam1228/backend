using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Dtos.Job;
using backend.Core.Entities;
using backend.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly RepositoryJob _job;
        public JobController(ApplicationDbContext context, IMapper mapper)
        {
            _job = new RepositoryJob(context, mapper);
        }
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDto dto)
        {
            await _job.Create(dto);
            return Ok("Create Job");
        }
        
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJobs()
        {
            var getJob = await _job.Get();

            return Ok(getJob);
        }

        [HttpGet]
        [Route("GetCandidates")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidates(long idCandidate)
        {
            var getCompany = await _job.GetCandidate(idCandidate);

            return Ok(getCompany);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateJob([FromBody] JobUpdateDto dto)
        {
            await _job.Update(dto);

            return Ok("Update Job");
        }
       
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteJob([FromBody] long id)
        {
            await _job.Delete(id);

            return Ok(" Delete");
        }
        
    }
}
