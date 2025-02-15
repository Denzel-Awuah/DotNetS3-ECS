namespace DotNetWebAPIS3.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string Name { get; set; }
        
        public Instructor(int instructorId, string name)
        {
            InstructorId = instructorId;
            Name = name;
        }

    }
}
