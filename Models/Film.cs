using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PandaList.Models
{
    public class Film
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
