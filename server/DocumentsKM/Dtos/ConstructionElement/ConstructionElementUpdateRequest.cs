using System.ComponentModel.DataAnnotations;

namespace DocumentsKM.Dtos
{
    public class ConstructionElementUpdateRequest
    {
        public int? ProfileId { get; set; }

        public int? SteelId { get; set; }

        [Range(0.0f, 10000.0f, ErrorMessage = "Value should be greater than or equal to 0")]
        public float? Length { get; set; }

        public ConstructionElementUpdateRequest()
        {
            ProfileId = null;
            SteelId = null;
            Length = null;
        }
    }
}
