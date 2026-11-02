using System.ComponentModel.DataAnnotations;

namespace WebApplication_TH2_Lab6.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public int LearnerID { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstMidName { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }
        // Major? (nullable) để MVC không coi navigation property là bắt buộc khi bind form
        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
