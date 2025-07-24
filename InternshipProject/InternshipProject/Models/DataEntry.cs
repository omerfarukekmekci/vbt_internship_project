namespace InternshipProject.Models
{
    public class DataEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
