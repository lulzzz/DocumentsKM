using System.ComponentModel.DataAnnotations;

namespace DocumentsKM.Dtos
{
    public class MarkGeneralDataPointCreateRequest
    {
        [Required]
        public string Text { get; set; }
    }
}
