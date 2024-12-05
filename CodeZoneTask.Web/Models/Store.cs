using System.ComponentModel.DataAnnotations;

namespace CodeZoneTask.Web.Models
{
    public class Store
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
