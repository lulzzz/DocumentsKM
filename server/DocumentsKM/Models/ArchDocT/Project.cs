using System.ComponentModel.DataAnnotations;

namespace DocumentsKM.ArchDocT
{
    public class Project
    {
        // For now we will consider NOT NULL constraint for every field

        // Проект
        [Key]
        public ulong Id { get; set; }

        // ВидРаботы
        [Required]
        public uint Type { get; set; }

        // Название
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        // НазваниеДоп
        [Required]
        [MaxLength(255)]
        public string AdditionalName { get; set; }

        // БазСерия
        [Required]
        [MaxLength(20)]
        public string BaseSeries { get; set; }

        // Должн1
        [Required]
        [MaxLength(30)]
        public string Position1 { get; set; }

        // Утвердил1
        [Required]
        [MaxLength(20)]
        public string Approved1 { get; set; }

        // Должн2
        [Required]
        [MaxLength(30)]
        public string Position2 { get; set; }

        // Утвердил2
        [Required]
        [MaxLength(20)]
        public string Approved2 { get; set; }
    }
}