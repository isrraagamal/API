namespace Day1.DTO
{
    public class DepartmentWithStudentNames
    {
        public int Department_Number { get; set; }
        public string? Department_Name { get; set; }
        public string? Department_Location { get; set; }
        public string? Manager_Name { get; set; }
        public List<string> Students_Names { get; set; } = new List<string>();
    }
}
