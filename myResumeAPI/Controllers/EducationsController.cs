using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myResumeAPI.Contracts;
using myResumeAPI.Models;
using myResumeAPI.Repositories;

namespace myResumeAPI.Controllers
{
    [Route("api/myResumeAPI/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private readonly IRepository<Education> _educationRepository;
        private readonly IRepository<AboutMe> _aboutMeRepository;
        private readonly ILogger<EducationsController> _logger;

        public EducationsController(IRepository<Education> educationRepository, IRepository<AboutMe> aboutMeRepository, ILogger<EducationsController> logger)
        {
            _educationRepository = educationRepository;
            _aboutMeRepository = aboutMeRepository; ;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<List<EducationDTO>>>> GetEducations()
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound();
                }
                var results = await _educationRepository.FindAllAsync();

                if (results == null)
                {
                    return NotFound("No records found.");
                }
                return Ok(results.Select(a => EducationDTO(a)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest("An error occurred.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<EducationDTO>> GetEducation(long id)
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound();
                }
                var education =  await _educationRepository.FindAsync(id);

                if (education == null)
                {
                    return NotFound("Education not found");
                }

                return Ok(EducationDTO(education));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest("An error occurred.");
            }
        }
        [HttpGet("GetByUserId/{userId}")]
        public async Task<ActionResult<List<EducationDTO>>> GetEducationsByUserID(long userId)
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound();
                }
                var education =  await _educationRepository.FindByConditionAsync(a => a.UserId == userId);

                if (education == null)
                {
                    return NotFound("Education not found");
                }

                return Ok(education.Select(a => EducationDTO(a)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest("An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEducation(long id, EducationDTO educationDTO)
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound("An error occurred");
                }
                var entity = await _educationRepository.FindAsync(educationDTO.ID);
                if (entity == null)
                {
                    return NotFound($"Education id {educationDTO.ID} does not exist.");
                }

                entity.QualificationName = educationDTO.QualificationName;
                entity.QualificationLevel = educationDTO.QualificationLevel;
                entity.InstitutionName = educationDTO.InstitutionName;
                entity.FromDate = educationDTO.FromDate;
                entity.ToDate = educationDTO.ToDate;
                entity.Status = educationDTO.Status;

                _educationRepository.Update(entity);
                await _educationRepository.SaveChangesAsync();

            return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Education>> PostEducation(EducationDTO educationDTO)
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound("An error occurred");
                }

                var entity = await _aboutMeRepository.FindAsync(educationDTO.UserId);
                if (entity == null)
                {
                    return NotFound($"User id {educationDTO.UserId} does not exist.");
                }

                var educationEntity = new Education
                {
                    UserId = educationDTO.UserId,
                    QualificationLevel = educationDTO.QualificationLevel,
                    QualificationName = educationDTO.QualificationName,
                    InstitutionName = educationDTO.InstitutionName,
                    FromDate = educationDTO.FromDate,
                    ToDate = educationDTO.ToDate,
                    Status = educationDTO.Status,
                };

                 _educationRepository.Add(educationEntity);
               await _educationRepository.SaveChangesAsync();

                return CreatedAtAction("GetEducation", new { id = educationDTO.ID }, educationDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducation(long id)
        {
            try
            {
                if (_educationRepository == null)
                {
                    _logger.LogError("Repository _educationRepository is null.");
                    return NotFound("An error occurred.");
                }
                var education = await _educationRepository.FindAsync(id);
                if (education == null)
                {
                    return NotFound("Education not found");
                }

                _educationRepository.Delete(education);
                await _educationRepository.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        //private bool EducationExists(long id)
        //{
        //    return (_context.Educations?.Any(e => e.ID == id)).GetValueOrDefault();
        //}
        
       private static EducationDTO EducationDTO(Education education) =>
       new EducationDTO
       {
           ID = education.ID,
           UserId = education.UserId,
           QualificationLevel = education.QualificationLevel,   
           QualificationName = education.QualificationName, 
           InstitutionName = education.InstitutionName,
           FromDate = education.FromDate,   
           ToDate = education.ToDate,   
           Status = education.Status,   
       };
    }
}
