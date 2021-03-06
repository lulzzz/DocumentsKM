using System.ComponentModel.DataAnnotations;

namespace DocumentsKM.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string BaseSeries { get; set; }
    }
}
