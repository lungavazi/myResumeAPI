using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myResumeAPI.Contracts;
using myResumeAPI.Manager;
using myResumeAPI.Models;
using NuGet.Protocol;

namespace myResumeAPI.Controllers
{
    [Route("api/myResumeAPI/[controller]")]
    [ApiController]
    public class AboutMeController : ControllerBase
    {
        private readonly IRepository<AboutMe> _aboutMeRepository;
        private readonly IRepository<WorkHistory> _workRepository;
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<ContactReference> _contactReferenceRepository;
        private readonly ILogger<AboutMeController> _logger;
        private readonly IManagerRepository _managerRepository;
        public AboutMeController(IRepository<AboutMe> aboutMeRepository, ILogger<AboutMeController> logger, IManagerRepository managerRepository, IRepository<WorkHistory> workRepository, IRepository<Education> educationRepository, IRepository<ContactReference> contactReferenceRepository)
        {
            _aboutMeRepository = aboutMeRepository;
            _logger = logger;
            _managerRepository = managerRepository;
            _workRepository = workRepository;
            _educationRepository = educationRepository;
            _contactReferenceRepository = contactReferenceRepository;   
        }

        [HttpGet]
        public async Task<ActionResult<List<AboutMeDTO>>> GetAboutme()
        {
            try
            {
                var results = await _managerRepository.GetAboutme();              
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AboutMeDTO>> GetAboutMe(long id)
        {
            try
            {
                if (_aboutMeRepository == null)
                {
                    _logger.LogError("Repository _aboutMeRepository is null.");
                    return NotFound();
                }
                var aboutMe = await _aboutMeRepository.FindAsync(id);
                if (aboutMe == null)
                {
                    _logger.LogInformation($"No record found for user {id}", DateTime.UtcNow);
                    return NotFound("No record found.");
                }
                //var workEntities = await _workRepository.FindByConditionAsync(a => a.UserId == id);

                //if (workEntities != null)
                //    aboutMe.WorkHistory = workEntities;

                //var educationEntities = await _educationRepository.FindByConditionAsync(a => a.UserId == id);

                //if (educationEntities != null)
                //    aboutMe.Educations = educationEntities;
               
                //var contactReferenceEntities = await _contactReferenceRepository.FindByConditionAsync(a => a.UserId == id);

                //if (contactReferenceEntities != null)
                //    aboutMe.ContactReference = contactReferenceEntities;

                return Ok(ProfileDTO(aboutMe));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAboutMe(long id, AboutMeDTO aboutMe)
        {
            try
            {
                if (_aboutMeRepository == null || id != aboutMe.ID)
                {
                    return NotFound();
                }
                var entity = await _aboutMeRepository.FindAsync(aboutMe.ID);
                if (entity == null)
                {
                    return NotFound($"Cannot find ID {aboutMe.ID}.");
                }
                entity.Surname = aboutMe.Surname;
                entity.Name = aboutMe.Name;
                entity.Title = aboutMe.Title;
                entity.Address = aboutMe.Address;
                entity.UserName = aboutMe.UserName;
                entity.Telephone = aboutMe.Telephone;
                entity.IsProfileActive = aboutMe.IsProfileActive;
                entity.Email = aboutMe.Email;
                entity.Gender = aboutMe.Gender;

                _aboutMeRepository.Update(entity);
                await _aboutMeRepository.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AboutMeDTO>> PostAboutMe(AboutMeDTO aboutMe)
        {
            try
            {
                if (_aboutMeRepository == null)
                {
                    _logger.LogError("Repository _aboutMeRepository is null.");
                    return Problem("An error occurred.");
                }

                var aboutMeEntity = new AboutMe
                {
                    Surname = aboutMe.Surname,
                    Name = aboutMe.Name,
                    Title = aboutMe.Title,
                    Address = aboutMe.Address,
                    UserName = aboutMe.UserName,
                    Telephone = aboutMe.Telephone,
                    IsProfileActive = aboutMe.IsProfileActive,
                    Email = aboutMe.Email,
                    Gender = aboutMe.Gender,
                };

                _aboutMeRepository.Add(aboutMeEntity);
                await _aboutMeRepository.SaveChangesAsync();

                return CreatedAtAction("GetAboutMe", new { id = aboutMeEntity.ID }, aboutMe.ID = aboutMeEntity.ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public  async Task<ActionResult> DeleteAboutMe(long id)
        {
            try
            {
                if (_aboutMeRepository == null)
                {
                    _logger.LogError("Repository _aboutMeRepository is null.");
                    return NotFound();
                }
                var aboutMe = await _aboutMeRepository.FindAsync(id);
                if (aboutMe == null)
                {
                    return NotFound("No record found");
                }
                _aboutMeRepository.Delete(aboutMe);
                await _aboutMeRepository.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }                      

        private static AboutMeDTO ProfileDTO(AboutMe aboutMe) =>
        new AboutMeDTO
        {
            ID = aboutMe.ID,
            UserName = aboutMe.UserName,
            Title = aboutMe.Title,
            Name = aboutMe.Name,
            Surname = aboutMe.Name,
            Email = aboutMe.Email,
            Telephone = aboutMe.Telephone,
            Gender = aboutMe.Gender,
            Address = aboutMe.Address,
            IsProfileActive = aboutMe.IsProfileActive
        };
    }
}
