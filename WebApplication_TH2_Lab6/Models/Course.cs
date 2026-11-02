namespace WebApplication_TH2_Lab6.Models
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        // Bảng Course trong CSDL có CourseID IDENTITY(1,1) nên để EF tự sinh giá trị
        // (không dùng DatabaseGeneratedOption.None như file hướng dẫn)
        public int CourseID { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
