using Microsoft.EntityFrameworkCore;
using WebApplication_TH2_Lab6.Models;

namespace WebApplication_TH2_Lab6.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolContext(serviceProvider
                .GetRequiredService<DbContextOptions<SchoolContext>>()))
            {
                context.Database.EnsureCreated();
                if (context.Majors.Any())
                {
                    return;
                }

                var majors = new Major[] {
                    new Major{MajorName="IT"},
                    new Major{MajorName="Economics"},
                    new Major{MajorName="Mathematics"},
                };
                foreach (var major in majors)
                {
                    context.Majors.Add(major);
                }
                context.SaveChanges();

                // Dùng chính đối tượng vừa lưu để lấy ID thật (IDENTITY không đảm bảo bắt đầu từ 1)
                var learners = new Learner[] {
                    new Learner { FirstMidName = "Carson", LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2005-09-01"), MajorID = majors[0].MajorID },
                    new Learner { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2002-09-01"), MajorID = majors[1].MajorID }
                };
                foreach (Learner l in learners)
                {
                    context.Learners.Add(l);
                }
                context.SaveChanges();

                var courses = new Course[]{
                    new Course{Title="Chemistry",Credits=3},
                    new Course{Title="Microeconomics",Credits=3},
                    new Course{Title="Macroeconomics",Credits=3}
                };
                foreach (Course c in courses)
                {
                    context.Courses.Add(c);
                }
                context.SaveChanges();

                var enrollments = new Enrollment[]{
                    new Enrollment{LearnerID=learners[0].LearnerID,CourseID=courses[0].CourseID,Grade=5.5f},
                    new Enrollment{LearnerID=learners[0].LearnerID,CourseID=courses[1].CourseID,Grade=7.5f},
                    new Enrollment{LearnerID=learners[1].LearnerID,CourseID=courses[0].CourseID,Grade=3.5f},
                    new Enrollment{LearnerID=learners[1].LearnerID,CourseID=courses[2].CourseID,Grade=7f}
                };
                foreach (Enrollment e in enrollments)
                {
                    context.Enrollments.Add(e);
                }
                context.SaveChanges();
            }
        }
    }
}
