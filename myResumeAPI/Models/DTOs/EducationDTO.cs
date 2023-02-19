
namespace myResumeAPI.Models
{
    public class EducationDTO
    {
        public long ID { get; set; }
        public long UserId { get; set; }
        public string QualificationName { get; set; }
        public string InstitutionName { get; set; }
        public string QualificationLevel { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Status { get; set; }

    }
}
