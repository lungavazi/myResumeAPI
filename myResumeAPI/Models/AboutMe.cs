using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myResumeAPI.Models
{
    public class AboutMe
    {
        public long ID { get; set; }
        public string? UserName { get; set; }
        public string? Title { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public Char? Gender { get; set; }
        public string? Address { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public Boolean IsProfileActive { get; set; }
        [ForeignKey("UserId")]
        public IEnumerable<Education> Educations { get; set; }
        [ForeignKey("UserId")]
        public IEnumerable<ContactReference> ContactReference { get; set; }
        [ForeignKey("UserId")]
        public IEnumerable<WorkHistory> WorkHistory { get; set; }
    }
}
