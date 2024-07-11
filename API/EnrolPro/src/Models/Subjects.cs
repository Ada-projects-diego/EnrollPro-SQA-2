
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentEnrolmentNew.src.Models
{
    public class Subjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { set; get; }
        public string SubjectName { set; get; }
        public string SubjectDescription { set; get; }
        public Courses? Course { get; set; }
        [ForeignKey("Course")]
        public int? CourseId { set; get; }
        public Subjects(string SubjectName, string SubjectDescription)
        {
            this.SubjectName = SubjectName;
            this.SubjectDescription = SubjectDescription;
        }
    }
}
