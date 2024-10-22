using AutoMapper;
using backend.Core.Context;
using backend.Core.Dtos.Company;
using backend.Core.Dtos.Job;
using backend.Core.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly RepositoryCompany _company;
        public CompanyController(ApplicationDbContext context, IMapper mapper)
        {
            _company = new RepositoryCompany(context, mapper);
        }
        
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreateDto dto)
        {     
            await _company.Create(dto);
            return Ok("Create company");
        }
             
        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<CompanyGetDto>>> GetCompanies()
        {
            var getCompany = await _company.Get();

            return Ok(getCompany);
        }

        [HttpGet]
        [Route("GetJob")]
        public async Task<ActionResult<IEnumerable<JobGetDto>>> GetJob(long idJob)
        {
            var getCompany = await _company.GetJobs(idJob);

            return Ok(getCompany);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCompany([FromBody] CompanyUpdateDto dto)
        {   
            await _company.Update(dto);

            return Ok("Update company");
        }
       
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCompanies([FromBody] long id)
        {
            await _company.Delete(id);      

            return Ok(" Delete");
        }

        

    }
}
