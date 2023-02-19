using Microsoft.EntityFrameworkCore;

namespace myResumeAPI.Models
{
    public class ResumeAPIContext: DbContext
    {
        public ResumeAPIContext(DbContextOptions<ResumeAPIContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public DbSet<AboutMe> AboutMe { get; set; } 
        public DbSet<ContactReference> ContactReferences { get; set; }
        public DbSet<Education> Educations { get; set; } 
        public DbSet<WorkHistory> WorkHistory { get; set; }
    }
}
