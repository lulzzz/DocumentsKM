using DocumentsKM.Models;

namespace DocumentsKM.Dtos
{
    public class MarkBaseResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual Department Department { get; set; }
    }
}
