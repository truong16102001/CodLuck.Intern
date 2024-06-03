using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4.Linq.Entities
{
    public static class Initializer
    {
        public static List<Student> InitStudents()
        {
            List<Student> students = new List<Student>
            {
                new Student { Id = 1, Name = "Alice Johnson", Email = "alice.johnson@example.com", Phone = "123-456-7890", Dob = new DateTime(1998, 1, 15), Address = "123 Maple St", IsGraduated = false },
                new Student { Id = 2, Name = "Bob Smith", Email = "bob.smith@example.com", Phone = "987-654-3210", Dob = new DateTime(1997, 2, 20), Address = "456 Oak St", IsGraduated = true },
                new Student { Id = 3, Name = "Charlie Brown", Email = "charlie.brown@example.com", Phone = "555-123-4567", Dob = new DateTime(1999, 3, 25), Address = "789 Pine St", IsGraduated = false },
                new Student { Id = 4, Name = "Daisy Ridley", Email = "daisy.ridley@example.com", Phone = "111-222-3333", Dob = new DateTime(2000, 4, 10), Address = "321 Elm St", IsGraduated = false },
                new Student { Id = 5, Name = "Ethan Hunt", Email = "ethan.hunt@example.com", Phone = "444-555-6666", Dob = new DateTime(1996, 5, 5), Address = "654 Birch St", IsGraduated = true },
                new Student { Id = 6, Name = "Fiona Gallagher", Email = "fiona.gallagher@example.com", Phone = "777-888-9999", Dob = new DateTime(1995, 6, 15), Address = "987 Cedar St", IsGraduated = true },
                new Student { Id = 7, Name = "George Orwell", Email = "george.orwell@example.com", Phone = "222-333-4444", Dob = new DateTime(2001, 7, 25), Address = "123 Spruce St", IsGraduated = false },
                new Student { Id = 8, Name = "Hannah Baker", Email = "hannah.baker@example.com", Phone = "333-444-5555", Dob = new DateTime(1998, 8, 10), Address = "456 Willow St", IsGraduated = true },
                new Student { Id = 9, Name = "Ian Somerhalder", Email = "ian.somerhalder@example.com", Phone = "888-999-0000", Dob = new DateTime(1997, 9, 30), Address = "789 Cypress St", IsGraduated = false },
                new Student { Id = 10, Name = "Jack Sparrow", Email = "jack.sparrow@example.com", Phone = "111-222-0000", Dob = new DateTime(1996, 10, 20), Address = "321 Palm St", IsGraduated = true }
            };
            return students;
        }

        public static List<Course> InitCourses()
        {
            List<Course> courses = new List<Course>
            {
                new Course { Id = 1, Name = "Mathematics", Description = "An introduction to mathematical principles and techniques." },
                new Course { Id = 2, Name = "Physics", Description = "Fundamentals of physics, including mechanics, waves, and thermodynamics." },
                new Course { Id = 3, Name = "Chemistry", Description = "Study of the composition, properties, and reactions of matter." },
                new Course { Id = 4, Name = "Biology", Description = "Exploration of living organisms and their environments." },
                new Course { Id = 5, Name = "Computer Science", Description = "Introduction to computer programming and algorithms." },
                new Course { Id = 6, Name = "History", Description = "Survey of major events and themes in world history." },
                new Course { Id = 7, Name = "Literature", Description = "Study of significant works of literature from various periods." },
                new Course { Id = 8, Name = "Art", Description = "Examination of visual arts, including painting, sculpture, and design." },
                new Course { Id = 9, Name = "Economics", Description = "Principles of microeconomics and macroeconomics." },
                new Course { Id = 10, Name = "Philosophy", Description = "Introduction to philosophical thinking and major philosophical issues." }
            };
            return courses;
        }

        public static List<Enrollment> InitEnrollments()
        {
            List<Enrollment> enrollments = new List<Enrollment>
            {
                new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollDate = new DateTime(2023, 1, 10) },
                new Enrollment { Id = 2, StudentId = 2, CourseId = 2, EnrollDate = new DateTime(2023, 1, 15) },
                new Enrollment { Id = 3, StudentId = 3, CourseId = 3, EnrollDate = new DateTime(2023, 1, 20) },
                new Enrollment { Id = 4, StudentId = 4, CourseId = 4, EnrollDate = new DateTime(2023, 1, 25) },
                new Enrollment { Id = 5, StudentId = 5, CourseId = 5, EnrollDate = new DateTime(2023, 2, 1) },
                new Enrollment { Id = 6, StudentId = 6, CourseId = 6, EnrollDate = new DateTime(2023, 2, 5) },
                new Enrollment { Id = 7, StudentId = 7, CourseId = 7, EnrollDate = new DateTime(2023, 2, 10) },
                new Enrollment { Id = 8, StudentId = 8, CourseId = 8, EnrollDate = new DateTime(2023, 2, 15) },
                new Enrollment { Id = 9, StudentId = 1, CourseId = 9, EnrollDate = new DateTime(2023, 2, 20) },
                new Enrollment { Id = 10, StudentId = 10, CourseId = 10, EnrollDate = new DateTime(2023, 2, 25) },
                new Enrollment { Id = 11, StudentId = 1, CourseId = 2, EnrollDate = new DateTime(2023, 3, 1) },
                new Enrollment { Id = 12, StudentId = 2, CourseId = 3, EnrollDate = new DateTime(2023, 3, 5) },
                new Enrollment { Id = 13, StudentId = 2, CourseId = 4, EnrollDate = new DateTime(2023, 3, 10) },
                new Enrollment { Id = 14, StudentId = 4, CourseId = 5, EnrollDate = new DateTime(2023, 3, 15) },
                new Enrollment { Id = 15, StudentId = 5, CourseId = 6, EnrollDate = new DateTime(2023, 3, 20) },
                new Enrollment { Id = 16, StudentId = 6, CourseId = 7, EnrollDate = new DateTime(2023, 3, 25) },
                new Enrollment { Id = 17, StudentId = 7, CourseId = 8, EnrollDate = new DateTime(2023, 4, 1) },
                new Enrollment { Id = 18, StudentId = 8, CourseId = 9, EnrollDate = new DateTime(2023, 4, 5) },
                new Enrollment { Id = 19, StudentId = 9, CourseId = 10, EnrollDate = new DateTime(2023, 4, 10) },
                 new Enrollment { Id = 20, StudentId = 10, CourseId = 1, EnrollDate = new DateTime(2023, 4, 15) }
            };
            return enrollments;
        }
    }
}
