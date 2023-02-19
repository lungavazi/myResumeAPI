using myResumeAPI.Models;

namespace myResumeAPI.Repositories
{
    public class EducationRepository: GenericRepository<Education>
    {
        public EducationRepository(ResumeAPIContext context): base(context)
        {

        }
    }
}
