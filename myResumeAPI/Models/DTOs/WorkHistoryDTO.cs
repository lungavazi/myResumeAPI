namespace myResumeAPI.Models.DTOs
{
    public class WorkHistoryDTO
    {
        public long ID { get; set; }
        public long UserId { get; set; }
        public string Position { get; set; }
        public string CompanyName { get; set; }
        public string WorkDescription { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsCurrentJob { get; set; }
    }
}
