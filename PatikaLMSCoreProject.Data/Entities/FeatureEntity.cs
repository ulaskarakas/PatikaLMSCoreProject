namespace PatikaLMSCoreProject.Data.Entities
{
    public class FeatureEntity : BaseEntity
    {
        public string Title { get; set; }
        // Relational Properties
        public ICollection<CourseFeatureEntity> CourseFeatures { get; set; }
    }
}