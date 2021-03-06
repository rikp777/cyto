using System;
using System.Web.Http;
using DAL.Context;
using DAL.Interfaces;
using Domain.Requests;
using LOGIC.Services.Company;

namespace API.Controllers.Company
{
    [RoutePrefix("api")]
    public class CompanyController : ApiController
    {
        private readonly CompanyService _companyService;

        public CompanyController()
        {
            _companyService = new CompanyService(new DatabaseContext());
        }

        public CompanyController(IDatabaseContext context)
        {
            _companyService = new CompanyService(context);
        }

        [HttpGet]
        [Route("companies")]
        public IHttpActionResult GetAll()
        {
            var results = _companyService.GetAll();
            if (results.Count == 0) return Ok("There are no companies found");

            return Ok(results);
        }

        [HttpGet]
        [Route("companies/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var result = _companyService.GetById(id);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [Route("companies")]
        public IHttpActionResult Create([FromBody] CompanyRequest companyToCreate)
        {
            if (companyToCreate == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            var temp = _companyService.GetByName(companyToCreate.Name);

            if (temp != null) return BadRequest("A company with that name already exists!");

            var result = _companyService.Create(companyToCreate);
            return Ok(result);
        }

        [HttpPut]
        [Route("companies/{id}")]
        public IHttpActionResult Update(int id, CompanyRequest companyRequest)
        {
            if (companyRequest == null)
            {
                return BadRequest("Empty request body!");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ControllerHelper.GetModelStateErrorMessages(ModelState));
            }

            var company = _companyService.GetById(id);
            if (company == null) return NotFound();

            var result = _companyService.Update(id, companyRequest);
            return Ok(result);
        }

        //TODO:
        [HttpDelete]
        [Route("companies/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var company = _companyService.GetById(id);
            if (company == null) return NotFound();
            var result = _companyService.Delete(id);
            return Ok(result);
        }
    }
}