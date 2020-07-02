using onlinelearningbackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlinelearningbackend.DAL
{
    public interface ICourseManager
    {
        IEnumerable<Course> GetAllCourses();
        Course CoursesByCourseId(int CourseId);
        IEnumerable<Course> CoursesByStudentId(string StudentId);
        IEnumerable<Course> CoursesByInstructorId(string InstructorId);
        IEnumerable<Course> CoursesByTrackId(int TrackId);
        //Course AddCourse(Course NewCourse, string InstructorId);
        IEnumerable<Course> AddCourse(Course course, string UserId, int TrackId);
        IEnumerable<Course> EditCourse(Course EditedCourse, string UserId, int TrackId);
        void DeleteCoursesByCourseId(int CourseId);
        void EnrollStudentInCourse(int CourseId, string StudentId);
        CourseMyUserModel IsUserEnrolled(int CourseId, string StudentId);

    }
}
