using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebESchoolData.Exceptions;
using WebESchoolData.Model;
using WebESchoolData.Repository;
using WebESchoolData.Services;

namespace WebESchool.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/school")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService  _schoolService;
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolService schoolService,ISchoolRepository schoolRepository)
        {
            _schoolService = schoolService;
            _schoolRepository = schoolRepository;
        }

        [Route("index")]
        [HttpGet]
        public async Task<dynamic> Index()
        {
            var data = _schoolRepository.GetAll().ToList();
            return data;
        }
        [Route("create")]
        [HttpPost]
        public async Task<dynamic> Create(School school)
        {
            try
            {
                var data = await _schoolRepository.AddAsync(school);
                return "Sucessfully";
            }
            catch (Exception e)
            {
                return e.Message;
                throw;
            }
        }

        [Route("update")]
        [HttpPut]
        public async Task<dynamic> update(School school)
        {
            var schoolDetail = _schoolRepository.GetById((int)school.Id) ?? throw new ItemNotFoundException($"School with id {school.Id} doesnot exists.");
            await _schoolRepository.UpdateAsync(school);
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async void delete(int id)
        {
            var schoolDetail = _schoolRepository.GetById((int)id) ?? throw new ItemNotFoundException($"School with id {id} doesnot exists.");
            schoolDetail.IsActive = false;
            schoolDetail.IsDelete = true;
            await _schoolRepository.UpdateAsync(schoolDetail);
        }
    }
}
