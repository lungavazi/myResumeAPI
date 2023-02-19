using myResumeAPI.Models;

namespace myResumeAPI.Repositories
{
    public class AboutMeRepository: GenericRepository<AboutMe>
    {
        public AboutMeRepository(ResumeAPIContext context) : base(context)
        {

        }
    }
}
