namespace DocumentsKM.Dtos
{
    public class NodeResponse
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public EmployeeBaseResponse ChiefEngineer { get; set; }
    }
}
