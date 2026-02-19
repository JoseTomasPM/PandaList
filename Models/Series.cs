using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PandaList.Models
{
    public class Series
    {
        public int Id { get; set; }
      
        public string Title { get; set; } = null!;

        public int Seasons { get; set; }

        public bool Finished { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
