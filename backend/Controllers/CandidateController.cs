using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Candidate;
using backend.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly RepositoryCandidate _candidate;      

        public CandidateController(ApplicationDbContext context, IMapper mapper)
        {            
            _candidate = new RepositoryCandidate(context, mapper);
        }
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCandidate([FromForm] CandidateCreateDto dto)
        {          
            await _candidate.Create(dto);            
            return Ok("Create Resume");          
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CandidateGetDto>>> GetCandidate()
        {
            var getCompany= await _candidate.Get();

            return Ok(getCompany);
        }
       
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCandidate([FromBody] CandidateUpdateDto dto)
        {   
            await _candidate.Update(dto);

            return Ok("Update Candidate");
        }
       
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCandidate([FromBody] long id)
        {
            await _candidate.Delete(id);

            return Ok(" Delete");
        }

    }
}
