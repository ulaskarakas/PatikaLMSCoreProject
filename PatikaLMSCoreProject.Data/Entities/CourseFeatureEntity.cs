namespace PatikaLMSCoreProject.Data.Entities
{
    public class CourseFeatureEntity : BaseEntity
    {
        public int CourseId { get; set; }
        public int FeatureId { get; set; }
        // Relational Properties
        public CourseEntity Course { get; set; }
        public FeatureEntity Feature { get; set; }
    }
}