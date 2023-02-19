using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myResumeAPI.Contracts;
using myResumeAPI.Models;
using myResumeAPI.Models.DTOs;
using myResumeAPI.Repositories;

namespace myResumeAPI.Controllers
{
    [Route("api/myResumeAPI/[controller]")]
    [ApiController]
    public class WorkHistoriesController : ControllerBase
    {
        private readonly IRepository<WorkHistory> _workHistoryRepository;
        private readonly IRepository<AboutMe> _aboutMeRepository;
        private readonly ILogger<AboutMeController> _logger;

        public WorkHistoriesController(IRepository<WorkHistory> workHistoryRepository, IRepository<AboutMe> aboutMeRepository, ILogger<AboutMeController> logger)
        {
            _workHistoryRepository = workHistoryRepository;
            _aboutMeRepository = aboutMeRepository; 
            _logger = logger;   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkHistoryDTO>>> GetWorkHistory()
        {
            try
            {
                if (_workHistoryRepository == null)
                {
                    _logger.LogError("Repository _workHistoryRepository is null.");
                    return NotFound();
                }
                var results = await _workHistoryRepository.FindAllAsync();

                if (results == null)
                {
                    return NotFound("No records found.");
                }
                return Ok(results.Select(a => WorkHistoryDTO(a)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest("An error occurred.");
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<WorkHistoryDTO>> GetWorkHistory(long id)
        {
          if (_workHistoryRepository == null)
          {
                _logger.LogError("Repository _workHistoryRepository is null.");
                return NotFound();
          }
            var workHistory = await _workHistoryRepository.FindAsync(id);

            if (workHistory == null)
            {
                return NotFound($"Work history id {id} does not exist.");
            }

            return Ok(WorkHistoryDTO(workHistory));
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<ActionResult<List<WorkHistoryDTO>>> GetWorkHistoryByUserId(long userId)
        {
          if (_workHistoryRepository == null)
          {
                _logger.LogError("Repository _workHistoryRepository is null.");
                return NotFound();
          }
            var workHistory = await _workHistoryRepository.FindByConditionAsync(a => a.UserId == userId);

            if (workHistory == null)
            {
                return NotFound($"Work history with user id {userId} does not exist.");
            }

            return Ok(workHistory.Select(a => WorkHistoryDTO(a)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkHistory(long id, WorkHistoryDTO workHistoryDTO)
        {
            try
            {
                if (_workHistoryRepository == null)
                {
                    _logger.LogError("Repository _workHistoryRepository is null.");
                    return NotFound("An error occurred.");
                }
                var workEntity = await _workHistoryRepository.FindAsync(id);    
                if (workEntity == null)
                {
                    return NotFound($"Work id {id} does not exist.");
                }

                workEntity.Position = workHistoryDTO.Position;
                workEntity.CompanyName = workHistoryDTO.CompanyName;
                workEntity.WorkDescription = workHistoryDTO.WorkDescription;
                workEntity.FromDate = workHistoryDTO.FromDate;
                workEntity.ToDate = workHistoryDTO.ToDate;
                workEntity.IsCurrentJob = workHistoryDTO.IsCurrentJob;

                _workHistoryRepository.Update(workEntity);
                await _workHistoryRepository.SaveChangesAsync();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }            
        }

        [HttpPost]
        public async Task<ActionResult<WorkHistoryDTO>> PostWorkHistory(WorkHistoryDTO workHistoryDTO)
        {
            try
            {
                if (_workHistoryRepository == null)
                {
                    _logger.LogError("Repository _workHistoryRepository is null.");
                    return NotFound("An error occurred.");
                }

                var aboutMe = await _aboutMeRepository.FindAsync(workHistoryDTO.UserId);

                if (aboutMe == null)
                {
                    return NotFound($"UserId {workHistoryDTO.UserId} does not exist.");
                }

                var workHistoryEntity = new WorkHistory
                {
                    UserId = workHistoryDTO.UserId,
                    CompanyName = workHistoryDTO.CompanyName,
                    WorkDescription = workHistoryDTO.WorkDescription,
                    FromDate = workHistoryDTO.FromDate,
                    ToDate = workHistoryDTO.ToDate,
                    IsCurrentJob = workHistoryDTO.IsCurrentJob,
                    Position = workHistoryDTO.Position,
                };

                _workHistoryRepository.Add(workHistoryEntity);
                await _workHistoryRepository.SaveChangesAsync();

                return CreatedAtAction("GetWorkHistory", new { id = workHistoryDTO.ID }, workHistoryDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkHistory(long id)
        {
            try
            {
                if (_workHistoryRepository == null)
                {
                    _logger.LogError("Repository _workHistoryRepository is null.");
                    return NotFound("An error occurred.");
                }
                var workHistory = await _workHistoryRepository.FindAsync(id);
                if (workHistory == null)
                {
                    return NotFound($"Work id {id} does not exist.");
                }

                _workHistoryRepository.Delete(workHistory);
                await _workHistoryRepository.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        private static WorkHistoryDTO WorkHistoryDTO(WorkHistory workHistory) =>
       new WorkHistoryDTO
       {
           ID = workHistory.ID,
           UserId = workHistory.UserId,
           Position = workHistory.Position,
           CompanyName = workHistory.CompanyName,
           WorkDescription = workHistory.WorkDescription,
           FromDate = workHistory.FromDate,
           ToDate = workHistory.ToDate,
           IsCurrentJob = workHistory.IsCurrentJob,
       };
    }
}
