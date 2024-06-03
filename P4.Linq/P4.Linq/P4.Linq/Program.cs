using P4.Linq.Entities;

var students = Initializer.InitStudents();

//Select: trả về danh sách tên của svien
var studentNames = students.Select(student => student.Name).ToList();
//where: return nhung svien da tot nghiep
var graduatedStudents = students.Where(student => student.IsGraduated).ToList();
//First: return first value in list, if not exist, it will throw exception
var firstStudent = students.First();
//firstOrDefault: return first value or null
var firstName = students.FirstOrDefault(s => s.Name.StartsWith("A"));
//Last, LastOrDefault
var lastStudent = students.Last();
var lastOrDefaultStudent = students.LastOrDefault(s => s.IsGraduated);

// Single: Trả về sinh viên với Id là 3. Nếu không có sinh viên nào hoặc có nhiều hơn một sinh viên thỏa mãn điều kiện,
// sẽ ném ra ngoại lệ.
var singleStudent = students.Single(s => s.Id == 3);

//Trả về sinh viên với Id là 11. Nếu không có sinh viên nào thỏa mãn điều kiện, sẽ trả về null.
//Nếu có nhiều hơn một sinh viên thỏa mãn điều kiện, sẽ ném ra ngoại lệ.
var singleOrDefaultStudent = students.SingleOrDefault(s => s.Id == 11);

//Kiểm tra xem tất cả sinh viên có đều đã tốt nghiệp hay không.
var allStudentsGraduated = students.All(s => s.IsGraduated);

//Kiểm tra xem có ít nhất một sinh viên đã tốt nghiệp hay không.
var anyGraduatedStudents = students.Any(s => s.IsGraduated);

// Sắp xếp các sinh viên theo tên a-z
var orderedStudents = students.OrderBy(s => s.Name);


//join
var enrollments = Initializer.InitEnrollments();
var courses = Initializer.InitCourses();

var queryResult = from s in students
                  join e in enrollments on s.Id equals e.StudentId
                  join c in courses on e.CourseId equals c.Id
                  select new
                  {
                      StudentID = s.Id,
                      StudentName = s.Name,
                      CourseId = c.Id,
                      CourseName = c.Name,
                      EnrolledDate = e.EnrollDate
                  };
foreach (var item in queryResult)
{
    Console.WriteLine($"Student ID: {item.StudentID}, Student Name: {item.StudentName}, Course ID: {item.CourseId}, Course Name: {item.CourseName}, Enrolled Date: {item.EnrolledDate.ToShortDateString()}");
}

//Trả về các sinh viên đã đăng ký môn học với ID là 2 (Physics).
var enrolledInPhysics = students.Where(s => enrollments.Select(e => e.CourseId).Contains(2));

//Group Nhóm các sinh viên theo trạng thái tốt nghiệp hoặc chưa tốt nghiệp.
var studentsByGraduation = students.GroupBy(s => s.IsGraduated);

Console.WriteLine("----------------------------------------------------------------");
//Hiển thị thông tin học sinh đăng ký nhiều khóa học nhất
var query = from s in students
            join e in enrollments on s.Id equals e.StudentId
            group e by new { SID  = e.StudentId, SName = s.Name } into g // group by SID và SName lưu vào bảng tạm g
            let count = g.Count() 
            orderby count descending
            select new
            {
                StudentID = g.Key.SID,
                StudentName = g.Key.SName,
                TotalCourses = count
            };

var maxCount = query.First().TotalCourses;

var studentsWithMaxCourses = query.Where(x => x.TotalCourses == maxCount);

foreach (var item in studentsWithMaxCourses)
{
    Console.WriteLine($"Student ID: {item.StudentID}, Student Name: {item.StudentName}, Total Courses: {item.TotalCourses}");
}




