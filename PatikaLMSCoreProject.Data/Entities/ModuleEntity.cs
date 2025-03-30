namespace PatikaLMSCoreProject.Data.Entities
{
    public class ModuleEntity : BaseEntity
    {
        public string ModuleName { get; set; }
        public int CourseId { get; set; }
        // Relational Properties
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
        public CourseEntity Course { get; set; }
    }
}