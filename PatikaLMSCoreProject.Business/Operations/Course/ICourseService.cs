using PatikaLMSCoreProject.Business.Operations.Course.Dtos;
using PatikaLMSCoreProject.Business.Types;

namespace PatikaLMSCoreProject.Business.Operations.Course
{
    public interface ICourseService
    {
        Task<ServiceMessage> AddCourse(AddCourseDto course);
    }
}