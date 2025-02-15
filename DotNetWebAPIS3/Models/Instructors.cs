namespace DotNetWebAPIS3.Models
{
    public class Instuctor
    {
        public int InstructorId { get; set; }

        public string Name { get; set; }
        
        public Instuctor(int instructorId, string name)
        {
            InstructorId = instructorId;
            Name = name;
        }

    }
}
