using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PandaList.Models
{
    public class Series
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int Seasons { get; set; }

        public bool Finished { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
