using System.ComponentModel.DataAnnotations;

namespace DapperProject.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
    }
}
