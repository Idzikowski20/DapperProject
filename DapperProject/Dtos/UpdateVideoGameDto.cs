namespace DapperProject.Dtos
{
    public class UpdateVideoGameDto
    {
        public string Title { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        public string Developer { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
    }
}
