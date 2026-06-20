namespace try4.Models
{
    public class SearchDto
    {
        public int SearchId { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int FromId { get; set; }
        public string FromCenterName { get; set; } = string.Empty;
        public int ToId { get; set; }
        public string ToCenterName { get; set; } = string.Empty;
        public int Seats { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
