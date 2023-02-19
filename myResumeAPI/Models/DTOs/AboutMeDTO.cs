using myResumeAPI.Models.DTOs;

namespace myResumeAPI.Models
{
    public class AboutMeDTO
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public Char? Gender { get; set; }
        public string? Address { get; set; }
        public Boolean IsProfileActive { get; set; }
        public IEnumerable<EducationDTO> Educations { get; set; }
        public IEnumerable<ContactReferenceDTO> ContactReference { get; set; }
        public IEnumerable<WorkHistoryDTO> WorkHistory { get; set; }
    }
}
