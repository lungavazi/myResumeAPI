using myResumeAPI.Models;

namespace myResumeAPI.Repositories
{
    public class ContactReferenceRepository: GenericRepository<ContactReference>
    {
        public ContactReferenceRepository(ResumeAPIContext context ) : base(context)
        {

        }
    }
}
