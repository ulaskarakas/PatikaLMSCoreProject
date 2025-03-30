using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PatikaLMSCoreProject.Data.Entities
{
    public class FeatureEntity : BaseEntity
    {
        public string Title { get; set; }
        // Relational Properties
        public ICollection<CourseFeatureEntity> CourseFeatures { get; set; }
    }

    public class FeatureConfiguration : BaseConfiguration<FeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<FeatureEntity> builder)
        {
            base.Configure(builder);
        }
    }
}