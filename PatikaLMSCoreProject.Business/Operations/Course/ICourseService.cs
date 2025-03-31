using PatikaLMSCoreProject.Business.Operations.Course.Dtos;
using PatikaLMSCoreProject.Business.Types;

namespace PatikaLMSCoreProject.Business.Operations.Course
{
    public interface ICourseService
    {
        Task<ServiceMessage> AddCourse(AddCourseDto course);
        Task<CourseDto> GetCourse(int id);
        Task<List<CourseDto>> GetCourses();
        Task<ServiceMessage> AdjustCourseStars(int id, int changeTo);
        Task<ServiceMessage> DeleteCourse(int id);
        Task<ServiceMessage> UpdateCourse(UpdateCourseDto course);
    }
}