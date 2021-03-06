using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DocumentsKM.Models
{
    public class Specification
    {
        // Поз_выпуска
        [Key]
        public int Id { get; set; }

        // Id_марки
        [Required]
        [ForeignKey("MarkId")]
        public virtual Mark Mark { get; set; }
        // public int MarkId { get; set; }

        // выпуск
        [Required]
        public int Num { get; set; }

        // тек_выпуск
        [Required]
        public bool IsCurrent { get; set; }

        // прим
        [MaxLength(255)]
        public string Note { get; set; }

        // дата_созд
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedDate { get; set; }

        public virtual IList<Construction> Constructions { get; set; } = new List<Construction>();

        public virtual IList<StandardConstruction> StandardConstructions { get; set; } = new List<StandardConstruction>();
    }
}
