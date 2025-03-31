using PatikaLMSCoreProject.Data.Enums;

namespace PatikaLMSCoreProject.Business.Operations.Course.Dtos
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stars { get; set; }
        public EducationType EducationType { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}