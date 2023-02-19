using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using myResumeAPI.Contracts;
using myResumeAPI.Models;
using myResumeAPI.Models.DTOs;

namespace myResumeAPI.Controllers
{
    [Route("api/myResumeAPI/[controller]")]
    [ApiController]
    public class ContactReferencesController : ControllerBase
    {
        private readonly IRepository<ContactReference> _contactRefRepository;
        private readonly IRepository<AboutMe> _aboutMeRepository;
        private readonly ILogger<ContactReferencesController> _logger;

        public ContactReferencesController(IRepository<ContactReference> contactRefRepository, IRepository<AboutMe> aboutMeRepository, ILogger<ContactReferencesController> logger)
        {
            _contactRefRepository = contactRefRepository;
            _aboutMeRepository = aboutMeRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactReferenceDTO>>> GetContactReferences()
        {
            try
            {
                if (_contactRefRepository == null)
                {
                    _logger.LogError("Repository _contactRefRepository is null.");
                    return NotFound();
                }

                var results = await _contactRefRepository.FindAllAsync();

                if (results == null)
                {
                    return NotFound("No records found");
                }

                return Ok(results.Select(a => contactReferenceDTO(a)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactReference>> GetContactReference(long id)
        {
            try
            {
                if (_contactRefRepository == null)
                {
                    _logger.LogError("Repository _contactRefRepository is null.");
                    return NotFound();
                }
                var results = await _contactRefRepository.FindAsync(id);

                if (results == null)
                {
                    return NotFound($"Contact reference id {id} does not exist.");
                }

                return Ok(contactReferenceDTO(results));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactReference(long id, ContactReferenceDTO contactReferenceDTO)
        {
            try
            {
                if (_contactRefRepository == null)
                {
                    _logger.LogError("Repository _contactRefRepository is null.");
                    return NotFound();
                }

                var contactRefEntity = await _contactRefRepository.FindAsync(contactReferenceDTO.ID);
                if (contactRefEntity == null)
                {
                    return NotFound($"Contact Reference Id {contactReferenceDTO.ID} does not exist.");
                }
                contactRefEntity.Name = contactReferenceDTO.Name;
                contactRefEntity.Title = contactReferenceDTO.Title;
                contactRefEntity.Telephone = contactReferenceDTO.Telephone;
                contactRefEntity.Relationship = contactReferenceDTO.Relationship;
                contactRefEntity.EmailAddre = contactReferenceDTO.EmailAddre;

                _contactRefRepository.Update(contactRefEntity);
                await _contactRefRepository.SaveChangesAsync();

                return Ok("Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ContactReferenceDTO>> PostContactReference(ContactReferenceDTO contactReferenceDTO)
        {
            try
            {
                if (_contactRefRepository == null)
                {
                    _logger.LogError("Repository _contactRefRepository is null.");
                    return NotFound();
                }

                var aboutMe = await _aboutMeRepository.FindAsync(contactReferenceDTO.UserId);

                if (aboutMe == null)
                {
                    return NotFound($"UserId {contactReferenceDTO.UserId} does not exist.");
                }

                var contactRefEntity = new ContactReference
                {
                    UserId = contactReferenceDTO.UserId,
                    Title = contactReferenceDTO.Title,
                    Name = contactReferenceDTO.Name,
                    Telephone = contactReferenceDTO.Telephone,
                    Relationship = contactReferenceDTO.Relationship,
                    EmailAddre = contactReferenceDTO.EmailAddre,
                };
                _contactRefRepository.Add(contactRefEntity);
                await _contactRefRepository.SaveChangesAsync();

                return CreatedAtAction("GetContactReference", new { id = contactReferenceDTO.ID }, contactReferenceDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactReference(long id)
        {
            try
            {
                if (_contactRefRepository == null)
                {
                    _logger.LogError("Repository _contactRefRepository is null.");
                    return NotFound();
                }

                var contactReference = await _contactRefRepository.FindAsync(id);
                if (contactReference == null)
                {
                    return NotFound("Contact reference does not exist.");
                }

                _contactRefRepository.Delete(contactReference);
                await _contactRefRepository.SaveChangesAsync();

                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return NotFound("An error occurred.");
            }
        }

        //private bool ContactReferenceExists(long id)
        //{
        //    return (_context.ContactReferences?.Any(e => e.ID == id)).GetValueOrDefault();
        //}

        private static ContactReferenceDTO contactReferenceDTO(ContactReference contactReference) =>
            new ContactReferenceDTO
            {
                ID = contactReference.ID,
                UserId = contactReference.UserId,
                Name = contactReference.Name,
                Title = contactReference.Title,
                Relationship = contactReference.Relationship,
                Telephone = contactReference.Telephone,
                EmailAddre = contactReference.EmailAddre            
            };
    }
}
