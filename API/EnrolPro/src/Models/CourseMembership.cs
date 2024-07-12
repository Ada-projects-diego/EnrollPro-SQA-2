namespace StudentEnrolmentNew.src.Models
{
    public class CourseMembership
    {
        public int StudentId { get; set; }
        public Students? Student { get; set; }
        public int CourseId { get; set; }
        public Courses? Course { get; set; }
    }
}
