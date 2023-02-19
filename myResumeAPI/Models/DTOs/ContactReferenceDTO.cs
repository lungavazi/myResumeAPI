namespace myResumeAPI.Models.DTOs
{
    public class ContactReferenceDTO
    {
        public long ID { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Telephone { get; set; }
        public string EmailAddre { get; set; }
    }
}
