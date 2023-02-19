using myResumeAPI.Models;

namespace myResumeAPI.Repositories
{
    public class WorkHistoryRepository: GenericRepository<WorkHistory>
    {
        public WorkHistoryRepository(ResumeAPIContext context): base(context)
        {

        }
    }
}
