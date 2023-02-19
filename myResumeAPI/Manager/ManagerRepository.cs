using Microsoft.AspNetCore.Mvc;
using myResumeAPI.Contracts;
using myResumeAPI.Controllers;
using myResumeAPI.Models;
using myResumeAPI.Repositories;

namespace myResumeAPI.Manager
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly IRepository<AboutMe> _aboutMeRepository;
        private readonly ILogger<ManagerRepository> _logger;

        public ManagerRepository(IRepository<AboutMe> aboutMeRepository, ILogger<ManagerRepository> logger)
        {
            _aboutMeRepository = aboutMeRepository;
            _logger = logger;
        }

        public async Task<ActionResult> GetAboutme()
        {
            try
            {
                if (_aboutMeRepository == null)
                {
                    _logger.LogError($"Repository {nameof(_aboutMeRepository)} is null.");
                    return new NotFoundObjectResult("An error occurred while fetching information.");
                }
                var aboutMe = await _aboutMeRepository.FindAllAsync();
                if (aboutMe == null)
                {
                    return new NotFoundObjectResult("An error occurred while fetching information.");
                }
                return new OkObjectResult(aboutMe.Select(a => ProfileDTO(a)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return new NotFoundObjectResult("An error occurred while fetching information.");
            }
        }

        public async Task<ActionResult> GetAboutMe(long id)
        {
            try
            {
                if (_aboutMeRepository == null)
                {
                    _logger.LogError($"Repository {nameof(_aboutMeRepository)} is null.");
                    return new NotFoundObjectResult("An error occurred while fetching information.");
                }
                var aboutMe = await _aboutMeRepository.FindAsync(id);
                if (aboutMe == null)
                {
                    _logger.LogInformation($"No record found for user {id}", DateTime.UtcNow);
                    return new NotFoundObjectResult("No record found.");
                }
                return new OkObjectResult(ProfileDTO(aboutMe));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                //return new NotFoundObjectResult("An error occurred.");
                throw;
            }
        }
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

